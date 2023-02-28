using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    public class UserTbl
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public bool IsActive { get; set; }

        public DateTime? LastLoggedIn { get; set; }

        public string Password { get; set; }

        public string Roles { get; set; }

        /// <summary>
        /// every time the user changes his Password,
        /// or an admin changes his Roles or stat/IsActive,
        /// create a new `SerialNumber` GUID and store it in the DB.
        /// </summary>
        public string SerialNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        [MaxLength(350)]
        public string Address { get; set; }
        
        public string City { get; set; }
        [MaxLength(150)]
        public string Email { get; set; }
        public virtual ICollection<UserTokenTbl> UserTokenTbl { get; set; }



    }
}
