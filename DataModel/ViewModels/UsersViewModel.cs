using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ViewModels
{
     public class UsersViewModel : ErrorModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public DateTime? LastLoggedIn { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
        public string SerialNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
    }
}
