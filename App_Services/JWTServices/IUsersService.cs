using DataModel.Models;
using DataModel.ViewModels;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Services.JWTServices
{
    public interface IUsersService
    {

        string GetSerialNumber(int userId);
        UserTbl FindUser(string username, string password);
        UserTbl FindUser(int userId);
        void UpdateUserLastActivityDate(int userId);
        void RegisterUser(UsersViewModel reg);
        List<UsersViewModel> ShowUserWithDtail();
         List<UserNameViewMdel> ShowUserName();
         List<UsersViewModel> SearchByUserName(UserNameViewMdel m);
         List<UsersViewModel> SearchByMobile(MobileViewModel m);
         UsersViewModel Edit( UsersViewModel m);



    }
}
