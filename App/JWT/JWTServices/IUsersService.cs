using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.JWT.JWTServices
{
    public interface IUsersService
    {
        string GetSerialNumber(int userId);
        IEnumerable<string> GetUserRoles(int userId);
        UserTbl FindUser(string username, string password);
        UserTbl FindUser(int userId);
        void UpdateUserLastActivityDate(int userId);
    }
}
