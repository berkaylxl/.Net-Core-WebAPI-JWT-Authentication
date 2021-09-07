
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorizationApp.DependencyResolvers
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers (this IServiceCollection serviceCollection,ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);  
            }
            return ServiceTool.Create(serviceCollection);
        }
    }
}
