using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    public class UserTokenTbl
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        //public virtual ApplicationUser OwnerUser { get; set; }

        public string AccessTokenHash { get; set; }

        public DateTime AccessTokenExpirationDateTime { get; set; }

        public string RefreshTokenIdHash { get; set; }

        public string Subject { get; set; }

        public DateTime RefreshTokenExpiresUtc { get; set; }

        public string RefreshToken { get; set; }
        public virtual UserTbl UserTbl{ get; set; }

    }
}
