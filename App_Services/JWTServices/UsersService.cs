using App_Data.UnitOfWork;
using DataModel;
using DataModel.Models;
using DataModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Data.Entity;

namespace App_Services.JWTServices
{
    public class UsersService : IUsersService
    {
        private  readonly IUnitOfWork _unitOfWork;
        private readonly ISecurityService _securityService;
        public UsersService(ISecurityService securityService, IUnitOfWork unitOfWork)
        {
            _securityService = securityService;
            _unitOfWork = unitOfWork;
        }
        //TODO: replace it with `/*public IDbSet<User> Users {set;get;}*/`
        //private static readonly IList<UserTbl> _users = new List<UserTbl>
        //{
        //    // initial `seed`, just for the demo
        //    new UserTbl
        //    {
        //     UserId = 1,
        //     UserName = "Maryam",
        //     DisplayName = "مریم",
        //     IsActive = true,
        //     LastLoggedIn = null,
        //     Password = new SecurityService().GetSha256Hash("1234"),
        //     Roles = new []{ "user", "Admin" },
        //     SerialNumber = Guid.NewGuid().ToString("N")
        //    }
        //};



        public IDbSet<UserTbl> Users { set; get; }
        public void RegisterUser(UsersViewModel reg)
        {
          
            try
            {

                var Exist = _unitOfWork.UserTblRepository.Query(a => a.UserName == reg.UserName).Any();
                if (!Exist)
                {
                    var PersianCalendar = new PersianCalendar();
                    var user = new UserTbl();
                    user.Address = reg.Address;
                    user.UserName = reg.UserName;
                    user.Telephone = reg.Telephone;
                    user.Roles = "user" ;
                    user.IsActive = true;
                    user.LastLoggedIn = DateTime.Now;//PersianCalendar.ToDateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0, 0);
                    user.Password = new SecurityService().GetSha256Hash("1234");
                    user.SerialNumber = Guid.NewGuid().ToString("N");
                    user.Email = reg.Email;
                    user.FirstName = reg.FirstName;
                    user.LastName = reg.LastName;
                    _unitOfWork.UserTblRepository.Insert(user);
                    _unitOfWork.SaveChanges();
                  
                    reg.Messsage =  "Succesfully inserted" ;


                }
                else
                {
                   reg.Messsage = "User is exist" ;
      
                }
            }
            catch (Exception ex)
            {

               reg.ErrorModels.AddExceptionAndDetail(ex);
               
            }

            
        }

        public List<UsersViewModel> ShowUserWithDtail()
        {
            var result = new List<UsersViewModel>();
            var temp = new UsersViewModel();
            try
            {
                var item = _unitOfWork.UserTblRepository.Any(); //If User is exist 
                if (item)
                {
                    var user = _unitOfWork.UserTblRepository.GetAll(true).ToList();

                    foreach (var i in user)
                    {
                        var model = new UsersViewModel()
                        {
                            Address=i.Address,
                            Birthdate=i.Birthdate,
                            City=i.City,
                            DisplayName=i.DisplayName,
                            FirstName=i.FirstName,
                            LastLoggedIn=i.LastLoggedIn,
                            LastName = i.LastName,
                            Mobile =i.Mobile,
                            Password = i.Password,
                            Roles = i.Roles,
                            SerialNumber = i.SerialNumber,
                            Telephone = i.Telephone,
                            UserName = i.UserName,
                            Email=i.Email
                        };

                       result.Add(model);
                    }
                   
                }
                else
                {
                    temp.Messsage = "There is not anyUser";
                    result.Add(temp);
                 

                }
            }
            catch (Exception ex)
            {

             temp.ErrorModels.AddExceptionAndDetail(ex);


            }
            return result;
        }
        public List<UserNameViewMdel> ShowUserName()
        {
            var res =new List<UserNameViewMdel>();
            var temp = new UserNameViewMdel();
            try
            {
                var item = _unitOfWork.UserTblRepository.Any(); //If User is exist release them
                if (item)
                {
                    var user = _unitOfWork.UserTblRepository.GetAll(true).Select(a => a.UserName).ToList();

                    foreach (var i in user)
                    {
                        var model = new UserNameViewMdel()
                        {
                            UserName = i
                        };
                        res.Add(model);
                    }
                }
                else
                {
                    temp.Messsage = "There is not anyUser";

                    res.Add(temp);
                }
            }
            catch (Exception ex)
            {

               temp.ErrorModels.AddExceptionAndDetail(ex);
                res.Add(temp);
            }
            return res;
        }
        public List<UsersViewModel> SearchByUserName(UserNameViewMdel m)
        {
            var result = new List<UsersViewModel>();
            var temp = new UsersViewModel();
            try
            {
                var item = _unitOfWork.UserTblRepository.Query(a => a.UserName.Contains(m.UserName), true).ToList();
                if (item.Count > 0)
                {
                    foreach (var i in item)
                    {
                        var model = new UsersViewModel()
                        {
                            Address = i.Address,
                            Birthdate = i.Birthdate,
                            City = i.City,
                            DisplayName = i.DisplayName,
                            FirstName = i.FirstName,
                            LastLoggedIn = i.LastLoggedIn,
                            LastName = i.LastName,
                            Mobile = i.Mobile,
                            Password = i.Password,
                            Roles = i.Roles,
                            SerialNumber = i.SerialNumber,
                            Telephone = i.Telephone,
                            UserName = i.UserName,
                            Email=i.Email
                        };

                        result.Add(model);
                    }
      

                }
                else
                {
                    temp.Messsage= "There is not any User" ;
                    result.Add(temp);
                }
             
            }
            catch (Exception ex)
            {

               temp.ErrorModels.AddExceptionAndDetail(ex);
                result.Add(temp);
            }
            return result;
        }

        public List<UsersViewModel> SearchByMobile(MobileViewModel m)
        {
            var result = new List<UsersViewModel>();
            var temp = new UsersViewModel();
            try
            {
                var item = _unitOfWork.UserTblRepository.Query(a => a.Mobile==m.MobileNo, true).ToList();
                if (item.Count > 0)
                {
                    foreach (var i in item)
                    {
                        var model = new UsersViewModel()
                        {
                            Address = i.Address,
                            Birthdate = i.Birthdate,
                            City = i.City,
                            DisplayName = i.DisplayName,
                            FirstName = i.FirstName,
                            LastLoggedIn = i.LastLoggedIn,
                            LastName = i.LastName,
                            Mobile = i.Mobile,
                            Password = i.Password,
                            Roles = i.Roles,
                            SerialNumber = i.SerialNumber,
                            Telephone = i.Telephone,
                            UserName = i.UserName,
                            Email=i.Email
                        };

                        result.Add(model);
                    }


                }
                else
                {
                 temp.Messsage= "There is not any User" ;
                    result.Add(temp);

                }
            
            }
            catch (Exception ex)
            {

                temp.ErrorModels.AddExceptionAndDetail(ex);
                result.Add(temp);

            }
            return result;
        }
        public UsersViewModel Edit(UsersViewModel m)
        {
           var result = new UsersViewModel();

            var find = _unitOfWork.UserTblRepository.Query(a => a.UserId == m.UserId).FirstOrDefault();
            if (find!=null)
            {
                var PersianCalendar = new PersianCalendar();


                find.Address = m.Address;
                find.UserName = m.UserName;
                find.Telephone = m.Telephone;
                find.Roles = m.Roles;
                find.IsActive = m.IsActive;
                find.Password = new SecurityService().GetSha256Hash("1234");
                find.SerialNumber = Guid.NewGuid().ToString("N");


                _unitOfWork.SaveChanges();
              
               result.Messsage= "Succesfully inserted" ;


            }
            else
            {
                result.Messsage = "There is not any User" ;
            }
            return result;
        }



        public UserTbl FindUser(string username, string password)
        {
            //var passwordHash = _securityService.GetSha256Hash(password);
            //return _users.FirstOrDefault(x => x.UserName == username && x.Password == passwordHash);
            try
            {
                var passwordHash = _securityService.GetSha256Hash(password);
                var z = _unitOfWork.UserTblRepository.Query(x => x.UserName == username , true).FirstOrDefault();

                // var z = _unitOfWork.UserTblRepository.Query(x => x.UserName == username && x.Password == passwordHash, true).FirstOrDefault();
                //var u = Users.Any(x => x.UserName == username );

                //var z = Users.FirstOrDefault(x => x.UserName == username && x.Password == passwordHash);

                return z;
            }
            catch (Exception ex)
            {

                ex = new Exception();
            }
            return _unitOfWork.UserTblRepository.Query(x => x.UserName == username, true, null, null).FirstOrDefault();

        }
        public UserTbl FindUser(int userId)
        {
            //return _users.FirstOrDefault(x => x.UserId == userId);
            return _unitOfWork.UserTblRepository.Query(a => a.UserId == userId).FirstOrDefault();
        }

        public string GetSerialNumber(int userId)
        {
            var user = FindUser(userId);
            return user.SerialNumber;
        }



        public void UpdateUserLastActivityDate(int userId)
        {
            var user = FindUser(userId);
            user.LastLoggedIn = DateTime.UtcNow;
        }
    }
}
