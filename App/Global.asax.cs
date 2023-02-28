using App.Controller.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace App
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
         //  GlobalConfiguration.Configuration.MessageHandlers.Add(new ApiLogHandler());
           GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
