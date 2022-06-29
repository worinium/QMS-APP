using FTMS.Services.Model;
using QMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FTMS.Services.BLL
{
    public class QMSManager
    {
        #region Tablet-End
        public static List<QmsService> GetQmsService(string state)
        {
            try
            {
                using (QmsdbEntities context = new QmsdbEntities())
                {
                    var querry = context.qms_service.Where(x => x.region_code == state && x.active == true).Select(c => new QmsService()
                    {
                        ServiceID = c.service_id,
                        ServiceCode = c.qms_service_type.mr_code,
                        ServiceTypeDescription = c.qms_service_type.description,
                        RegionCode = c.qms_region.mr_code,
                        created_date = c.created_date,
                        active = c.active
                    }).OrderBy(c => c.created_date).ToList();
                    return querry;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ManageTokenView.GetQmsService: {0} " + ex.Message);
            }
        }

        public static int getNextTokenForService(int serviceID)
        {
            try
            {
                using (QmsdbEntities context = new QmsdbEntities())
                {
                    int tokenNumber = 0;
                    DateTime dt = DateTime.Now.Date;
                    var queueTokens = context.qms_queue.Where(x => x.service_id == serviceID && x.created_date > dt).ToList(); //get all tokens under this service
                    var currentRegion = context.qms_service.Where(t => t.service_id == serviceID).Select(t => t.region_code).FirstOrDefault(); //get region
                    var todayTokens = context.qms_queue.Where(x => x.created_date > dt && x.qms_service.region_code == currentRegion).ToList(); //get all tokens under this region

                    //If this is the first token of the day
                    if (queueTokens.Count == 0 && todayTokens.Count == 0)
                    {
                        tokenNumber = 1;
                    }
                    else if (queueTokens.Count == 0) //if this is the first token in this queue but this is not the first queue
                    {
                        int numServices = todayTokens.Select(t => t.service_id).Distinct().Count();
                        do
                        {
                            int nextNumber = numServices * 30; //jump in steps of 30
                            if (todayTokens.Where(t => t.queue_number == nextNumber).Count() == 0)
                            {
                                tokenNumber = nextNumber;
                                //If we reached a multiple of 30 which is not assigned, then just pick it
                                break;
                            }
                            //otherwise keep looking
                            numServices++;
                        } while (true);
                    }
                    else
                    {
                        int nextNumber = queueTokens.Max(t => t.queue_number) + 1;
                        do
                        {
                            if (todayTokens.Where(t => t.queue_number == nextNumber).Count() == 0)
                            {
                                tokenNumber = nextNumber;
                                //If we reached a multiple of 30 which is not assigned, then just pick it
                                break;
                            }
                            nextNumber = nextNumber + 30; //jump in steps of 30
                        } while (true);
                    }
                    qms_queue queue = new qms_queue();
                    queue.service_id = serviceID;
                    queue.queue_number = tokenNumber;
                    queue.created_date = DateTime.Now;
                    queue.modified_date = DateTime.Now;
                    queue.queue_status = Helpers.Constants.StatusWaiting;
                    queue.called = false;
                    context.qms_queue.Add(queue);
                    context.SaveChanges();
                    return tokenNumber;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ManageTokenView.getNextTokenForService: {0} " + ex.Message);
            }
        }
        #endregion Tablet-End

        #region WaitingRoom-End
        public static List<Model.QueueDetails> getRegionTokens(string regionCode)
        {
            try
            {
                using (QmsdbEntities context = new QmsdbEntities())
                {
                    DateTime dt = DateTime.Now.Date;
                    var todayTokens = context.qms_queue.Include("qms_service").Where(x => x.created_date > dt && x.qms_service.region_code == regionCode).ToList(); //get all tokens under this region
                    var regionServices = context.qms_service.Include("qms_service_type").Where(t => t.region_code == regionCode).ToList();
                    List<QueueDetails> queueSizes = todayTokens.GroupBy(t => t.qms_service.service_id).Select(t => new QueueDetails() { ServiceID = t.Key, QueueSize = t.Count(f => !f.called && f.queue_status == Helpers.Constants.StatusWaiting) }).ToList();
                    foreach (var queue in queueSizes)
                    {
                        queue.ServiceTypeDescription = regionServices.Where(t => t.service_id == queue.ServiceID).FirstOrDefault().qms_service_type.description;
                        var currentToken = todayTokens.Where(t => t.service_id == queue.ServiceID && t.called).OrderByDescending(t => t.queue_number).FirstOrDefault();
                        queue.CurrentToken = currentToken == null ? "---" : currentToken.queue_number.ToString();
                        queue.SeatNumber = currentToken == null ? "---" : currentToken.qms_seat.seat_number.ToString();
                        var nextToken = todayTokens.Where(t => t.service_id == queue.ServiceID && t.queue_status == Helpers.Constants.StatusWaiting).OrderBy(t => t.queue_number).FirstOrDefault();
                        queue.NextToken = nextToken == null ? "---" : nextToken.queue_number.ToString();
                    }
                    return queueSizes;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ManageTokenView.getRegionTokens: {0} " + ex.Message);
            }
        }

        public static qms_queue getCalledQueueTokens(string regionCode)
        {
            try
            {
                using (QmsdbEntities context = new QmsdbEntities())
                {
                    DateTime dt = DateTime.Now.Date;
                    var regionServices = context.qms_service.Include("qms_service_type").Where(t => t.region_code == regionCode).ToList();
                    //Out of today's tokens for this region, get all those which have not been calledand who are processing
                    //Pick the earliest one and return it
                    var nextTicket = context.qms_queue.Include("qms_seat").Include("qms_service")
                                    .Where(x => x.created_date > dt && x.qms_service.region_code == regionCode && !x.called && x.queue_status == Helpers.Constants.StatusProcessing)
                                    .OrderByDescending(t => t.created_date)
                                    .FirstOrDefault();
                    return nextTicket;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ManageTokenView.getTokenCallsTokens: {0} " + ex.Message);
            }
        }

        public static void flagQueueAsCalled(int queueID)
        {
            try
            {
                using (QmsdbEntities context = new QmsdbEntities())
                {
                    qms_queue calledTicket = context.qms_queue.Where(t => t.queue_id == queueID).FirstOrDefault();
                    calledTicket.called = true;
                    calledTicket.call_date = DateTime.Now;
                    calledTicket.modified_date = DateTime.Now;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ManageTokenView.getTokenCallsTokens: {0} " + ex.Message);
            }
        }
        #endregion WaitingRoom-End

        public static string getRegion(string region_code)
        {
            try
            {
                using (QmsdbEntities context = new QmsdbEntities())
                {
                    qms_region regionObj = context.qms_region.Where(s => s.mr_code == region_code && s.active).FirstOrDefault();
                    if (regionObj != null)
                        return regionObj.description;
                    else
                        return "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error While getting QmsSetting due to: ", ex.Message));
            }
        }
    }
}

