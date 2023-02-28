using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Services.JWTServices
{
    public interface ITokenStoreService
    {
        void CreateUserToken(UserTokenTbl userToken);
        bool IsValidToken(string accessToken, int userId);
        void DeleteExpiredTokens();
        UserTokenTbl FindToken(string refreshTokenIdHash);
        void DeleteToken(string refreshTokenIdHash);
        void InvalidateUserTokens(int userId);
        void UpdateUserToken(int userId, string accessTokenHash);
    }
}
