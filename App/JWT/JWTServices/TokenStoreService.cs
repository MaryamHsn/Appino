
using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.JWT.JWTServices
{
    public class TokenStoreService : ITokenStoreService
    {
        //TODO: replace it with `public IDbSet<UserToken> Tokens {set;get;}`
        private static readonly IList<UserTokenTbl> _tokens = new List<UserTokenTbl>();

        private readonly ISecurityService _securityService;
        public TokenStoreService(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        public void CreateUserToken(UserTokenTbl userToken)
        {
            InvalidateUserTokens(userToken.OwnerUserId); //Delete all previous token ,So just one person can login
            _tokens.Add(userToken);
        }

        public void UpdateUserToken(int userId, string accessTokenHash)
        {
            var token = _tokens.FirstOrDefault(x => x.OwnerUserId == userId);
            if (token != null)
            {
                token.AccessTokenHash = accessTokenHash;
            }
        }

        public void DeleteExpiredTokens()
        {
            var now = DateTime.UtcNow;
            var userTokens = _tokens.Where(x => x.RefreshTokenExpiresUtc < now).ToList();
            foreach (var userToken in userTokens)
            {
                _tokens.Remove(userToken);
            }
        }

        public void DeleteToken(string refreshTokenIdHash)
        {
            var token = FindToken(refreshTokenIdHash);
            if (token != null)
            {
                _tokens.Remove(token);
            }
        }

        public UserTokenTbl FindToken(string refreshTokenIdHash)
        {
            return _tokens.FirstOrDefault(x => x.RefreshTokenIdHash == refreshTokenIdHash);
        }


        //Deletet all  Token 
        public void InvalidateUserTokens(int userId)
        {
            var userTokens = _tokens.Where(x => x.OwnerUserId == userId).ToList();
            foreach (var userToken in userTokens)
            {
                _tokens.Remove(userToken);
            }
        }

        public bool IsValidToken(string accessToken, int userId)
        {
            var accessTokenHash = _securityService.GetSha256Hash(accessToken);
            var userToken = _tokens.FirstOrDefault(x => x.AccessTokenHash == accessTokenHash && x.OwnerUserId == userId);
            return userToken?.AccessTokenExpirationDateTime >= DateTime.UtcNow;
        }
    }
}
