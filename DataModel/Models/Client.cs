using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class Client
    {
        public Client()
        {
            PayLog = new HashSet<PayLog>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string NationalCode { get; set; }
        public string Phone { get; set; }
        public string TellPhone { get; set; }
        public DateTime  BirthDay { get; set; }
        public DateTime  IssueDate { get; set; }
        public DateTime  StartInsurenceDate { get; set; }
        public short  Period { get; set; }
        public DateTime  NextRemmeberDate { get; set; }
        public string VisitPlace { get; set; }
        public string Occupation { get; set; }
        public int  ConsulateCount { get; set; }
        public long  ParentClient { get; set; }
        public bool  IsRelative { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public int  RegionId { get; set; }
        public DateTime  AlartDate { get; set; }
        public string DocumentUploadedPath { get; set; }

        public Regin Region { get; set; }
        public ICollection<PayLog> PayLog { get; set; }
    }
}
