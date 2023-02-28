using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Services.JWTServices
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
            InvalidateUserTokens(userToken.UserId);
            _tokens.Add(userToken);
        }

        public void UpdateUserToken(int userId, string accessTokenHash)
        {
            var token = _tokens.FirstOrDefault(x => x.UserId == userId);
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

        public void InvalidateUserTokens(int userId)
        {
            var userTokens = _tokens.Where(x => x.UserId == userId).ToList();
            foreach (var userToken in userTokens)
            {
                _tokens.Remove(userToken);
            }
        }

        public bool IsValidToken(string accessToken, int userId)
        {
            var accessTokenHash = _securityService.GetSha256Hash(accessToken);
            var userToken = _tokens.FirstOrDefault(x => x.AccessTokenHash == accessTokenHash && x.UserId == userId);
            return userToken?.AccessTokenExpirationDateTime >= DateTime.UtcNow;
        }
    }
}
