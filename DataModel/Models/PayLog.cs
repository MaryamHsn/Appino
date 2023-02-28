using System;
using System.Collections.Generic;

namespace DataModel.Models

{
    public partial class PayLog
    {
        public long Id { get; set; }
        public decimal Cost { get; set; }
        public DateTime PayDate { get; set; }
        public long  ClientId { get; set; }
        public int IncreasePresent { get; set; }
        public string Guid { get; set; }

        public Client Client { get; set; }
    }
}
