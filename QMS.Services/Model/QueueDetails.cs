using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTMS.Services.Model
{
    public class QueueDetails
    {
        public int ServiceID { get; set; }
        public string ServiceTypeDescription { get; set; }
        public int QueueSize { get; set; }
        public string SeatNumber { get; set; }
        public string NextToken { get; set; }
        public string CurrentToken { get; set; }
    }
}
