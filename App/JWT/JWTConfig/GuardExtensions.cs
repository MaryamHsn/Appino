using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.JWT.JWTConfig
{
    public static class GuardExtensions
    {
        public static void CheckArgumentNull(this object o, string name)
        {
            if (o == null)
                throw new ArgumentNullException(name);
        }
    }
}