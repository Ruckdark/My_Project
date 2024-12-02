using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeMgmtSystem.Models
{
    public class Tree
    {
        public int TreeId { get; set; }
        public int UserId { get; set; }
        public int Species { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public int Diameter { get; set; }
        public string HealthStatus { get; set; }
        public string Note { get; set; }
        public string Location { get; set; }
        public DateTime ReminderDate { get; set; }
    }
}

