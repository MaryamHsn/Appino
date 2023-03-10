using App.JWT.JWTConfig;
using App_Data.UnitOfWork;
using App_Services.JWTServices;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using StructureMap;
using StructureMap.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace App.IoCConfig
{
    public class SmObjectFactory
    {
        private static readonly Lazy<Container> _containerBuilder =
           new Lazy<Container>(defaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container { get; } = _containerBuilder.Value;

        private static Container defaultContainer()
        {
            return new Container(ioc =>
            {
                ioc.For<IAppJwtConfiguration>().Singleton().Use(() => AppJwtConfiguration.Config);
                ioc.For<IUsersService>().HybridHttpOrThreadLocalScoped().Use<UsersService>();
                ioc.For<ITokenStoreService>().HybridHttpOrThreadLocalScoped().Use<TokenStoreService>();
                ioc.For<ISecurityService>().HybridHttpOrThreadLocalScoped().Use<SecurityService>();
                ioc.For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Use<UnitOfWork>();

                ioc.Policies.SetAllProperties(setterConvention =>
                {
                    // For WebAPI ActionFilter Dependency Injection
                    setterConvention.OfType<Func<IUsersService>>();
                    setterConvention.OfType<Func<ITokenStoreService>>();
                    setterConvention.OfType<Func<IUnitOfWork>>();
                });

                // we only need one instance of this provider
                ioc.For<IOAuthAuthorizationServerProvider>().Singleton().Use<AppOAuthProvider>();
                ioc.For<IAuthenticationTokenProvider>().Singleton().Use<RefreshTokenProvider>();
            });
        }
    }
}