using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.JWT.JWTServices
{
    public interface ISecurityService
    {
        string GetSha256Hash(string input);
    }
}