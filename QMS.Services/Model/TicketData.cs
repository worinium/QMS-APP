using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTMS.Services.Model
{
    public class TicketData
    {

        public string Date { get; set; }
        public string Time { get; set; }
        public string Agency { get; set; }
        public string DeskName { get; set; }
        public int TokenNumber { get; set; }
    }
}
