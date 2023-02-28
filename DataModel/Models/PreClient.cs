using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    public class PreClient
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string  Occupation { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime AlertDate { get; set; }

    }
}
