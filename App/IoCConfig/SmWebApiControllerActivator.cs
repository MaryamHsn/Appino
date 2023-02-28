using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace App.IoCConfig
{
    public class SmWebApiControllerActivator : IHttpControllerActivator
    {
        private readonly IContainer _container;
        public SmWebApiControllerActivator(IContainer container)
        {
            _container = container;
        }

        public IHttpController Create(
                HttpRequestMessage request,
                HttpControllerDescriptor controllerDescriptor,
                Type controllerType)
        {
            var nestedContainer = _container.GetNestedContainer();
            request.RegisterForDispose(nestedContainer);
            return (IHttpController)nestedContainer.GetInstance(controllerType);
        }
    }
}