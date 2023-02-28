using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class City
    {
        public City()
        {
            Regin = new HashSet<Regin>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Regin> Regin { get; set; }
    }
}
