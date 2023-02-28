using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class Regin
    {
        public Regin()
        {
            Client = new HashSet<Client>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }

        public City City { get; set; }
        public ICollection<Client> Client { get; set; }
    }
}
