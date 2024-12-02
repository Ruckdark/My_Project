using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeMgmtSystem.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public int Annunciator { get; set; }
        public DateTime ReportDate { get; set; }
        public string Description { get; set; }
    }
}

