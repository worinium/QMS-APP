using System;
using System.Drawing;
using System.Drawing.Printing;
using FTMS.Services;
using FTMS.Services.BLL;
namespace QMS.GUI
{
    public class ManageTicket
    {
        private int ticketNo;
        private DateTime TicketDate;
        private string _regionCode, agency, Service, printer;
        public int TicketNo
        {
            set { this.ticketNo = value; }
            get { return this.ticketNo; }
        }
        public DateTime ticketDate
        {
            set { this.TicketDate = value; }
            get { return this.TicketDate; }
        }
        public ManageTicket(int ticketNo, DateTime TicketDate, string _regionCode, string service, string printer)
        {
            this.ticketNo = ticketNo;
            this.TicketDate = TicketDate;
            this._regionCode = _regionCode;
            this.Service = service;
            this.printer = printer;
        }
        public void Tickprint()
        {
            try
            {
                PrintDocument pdoc = new PrintDocument();
                pdoc.DefaultPageSettings.PaperSize = new PaperSize("80 x 80 mm", 315, 315);
                pdoc.PrinterSettings.PrinterName = printer;
                pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
                pdoc.Print();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("ManageTicket.Tickprint: {0}", ex.Message));
            }

        }
        void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            try
            {
                int startX = 0, startY = 0, Offset = 0;
                agency = Helpers.getQmsSetting(Helpers.Constants.AgencySetting);
                string serviceCenter = QMSManager.getRegion(_regionCode);
                //For DrawString
                Rectangle rect1 = new Rectangle(startX, startY, 283, 40);
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                //For DrawLine 
                Pen pen = new Pen(new SolidBrush(Color.Black));
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                graphics.DrawString(agency, new Font("Tahoma", 13),
                                           new SolidBrush(Color.Black), rect1, stringFormat);
                Offset += 55;
                Rectangle rect3 = new Rectangle(startX, startY + Offset, 282, 20);
                graphics.DrawString(serviceCenter, new Font("Tahoma", 13),
                           new SolidBrush(Color.Black), rect3, stringFormat);
                Offset += 25;
                graphics.DrawLine(pen, 0, Offset, 283, Offset);

                Offset += 15;
                Rectangle rect4 = new Rectangle(startX, startY + Offset, 282, 20);
                graphics.DrawString(this.Service, new Font("Tahoma", 15),
                         new SolidBrush(Color.Black), rect4, stringFormat);
                Offset += 40;
                Rectangle rect5 = new Rectangle(startX, startY + Offset, 282, 20);
                graphics.DrawString("#" + this.TicketNo, new Font("Tahoma", 16),
                         new SolidBrush(Color.Black), rect5, stringFormat);
                Offset += 30;
                graphics.DrawLine(pen, 0, Offset, 283, Offset);
                Offset = Offset + 22;
                graphics.DrawString("Date : " + this.ticketDate, new Font("Tahoma", 12),
                         new SolidBrush(Color.Black), startX, startY + Offset);
                Offset += 28;
                graphics.DrawLine(pen, 0, Offset, 283, Offset);
            }
            catch (Exception ex)
            {
               throw new Exception("ManageTicket.pdoc_PrintPage: {0} " + ex.Message);
            }
            finally
            {
                graphics.Dispose(); 
            }
        }
    }
}
