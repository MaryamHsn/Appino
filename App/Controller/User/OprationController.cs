using App.JWT.JWTConfig;
using App_Services.JWTServices;
using DataModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace App.Controller.User
{
  //  [Route("api/op/[Action]")]
    public class OprationController : ApiController
    {

        private readonly IUsersService _usersService;

        public OprationController(IUsersService usersService)
        {
            this._usersService = usersService;
        }
        /// <summary>
        /// Register an User
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
         [Route("api/Reg")]
        [HttpPost]
        public void Register(UsersViewModel m)
        {
           _usersService.RegisterUser(m);
        }

        /// <summary>
        /// show user with Detail To Admin
        /// </summary>
        /// <returns></returns>
        [Route("api/show")]
        [HttpGet]
        [JwtAuthorize(Roles = "Admin")]
        public IHttpActionResult ShowUserWithDetail()
        {
            return Ok(_usersService.ShowUserWithDtail());
        }
        [Route("api/showuse")]
        [HttpGet]
        [JwtAuthorize(Roles = "Admin")]
        public IHttpActionResult ShowUserOnly()
        {
            return Ok(_usersService.ShowUserName());
        }
        /// <summary>
        /// Search By Name
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [Route("api/search/name")]
        [HttpPost]
        public List<UsersViewModel> SearchByName(UserNameViewMdel m)
        {
            var find = _usersService.SearchByUserName(m);
            var result = new List<UsersViewModel>();
            foreach (var item in find)
            {
                result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// search by mobile Number
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
       [Route("api/search/mob")]
        [HttpPost]
        public List<UsersViewModel> SearchByMobile(MobileViewModel m)
        {
            var find = _usersService.SearchByMobile(m);
            var result = new List<UsersViewModel>();
            foreach (var item in find)
            {
                result.Add(item);
            }

            return result;
        }

        [Route("api/edit")]
        [HttpPost]
        public UsersViewModel EditUser( UsersViewModel m)
        {
            var find = _usersService.Edit(m);
            return find;
        }
    }
}