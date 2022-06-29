using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTMS.Services.Model
{
    public class QmsService
    {
        public int ServiceID { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceTypeDescription { get; set; }
        public string RegionCode { get; set; }
        public DateTime created_date { get; set; }
        public bool active { get; set; }


    }
}
