using AuthorizationApp.Data;
using AuthorizationApp.Service;
using AuthorizationApp.Utilities.Security.JWT;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AuthorizationApp.Utilities.Interceptors.MethodInterception;

namespace AuthorizationApp.DependencyResolvers
{
    public class AutofacModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<IGenericRepository>().As<EfRepositoryBase>().SingleInstance();
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
                
        }
    }
}
