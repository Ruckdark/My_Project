using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeMgmtSystem.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string ServiceType { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
    }
}

