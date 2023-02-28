
//using DataModel.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace App.JWT.JWTServices
//{
//    public class UsersService : IUsersService
//    {
//        //TODO: replace it with `public IDbSet<User> Users {set;get;}`
//        private static readonly IList<UserTbl> _users = new List<UserTbl>
//        { 
//            // initial `seed`, just for the demo
//            new UserTbl
//            {
//             UserId = 1,
//             UserName = "Vahid",
//             DisplayName = "وحيد",
//             IsActive = true,
//             LastLoggedIn = null,
//             Password = new SecurityService().GetSha256Hash("1234"),
//             Roles = new []{ "user", "Admin" },
//             SerialNumber = Guid.NewGuid().ToString("N")
//            }
//        };

//        private readonly ISecurityService _securityService;
//        public UsersService(ISecurityService securityService)
//        {
//            _securityService = securityService;
//        }

//        public UserTbl FindUser(int userId)
//        {
//            return _users.FirstOrDefault(x => x.UserId == userId);
//        }

//        public UserTbl FindUser(string username, string password)
//        {
//            var passwordHash = _securityService.GetSha256Hash(password);
//            return _users.FirstOrDefault(x => x.UserName == username && x.Password == passwordHash);
//        }

//        public string GetSerialNumber(int userId)
//        {
//            var user = FindUser(userId);
//            return user.SerialNumber;
//        }

//        public IEnumerable<string> GetUserRoles(int userId)
//        {
//            var user = FindUser(userId);
//            return user.Roles;
//        }

//        public void UpdateUserLastActivityDate(int userId)
//        {
//            var user = FindUser(userId);
//            user.LastLoggedIn = DateTime.UtcNow;
//        }
//    }
//}