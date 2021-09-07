using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorizationApp.DependencyResolvers
{
   public  interface ICoreModule
    {
        void Load(IServiceCollection serviceCollection);
    }
}
