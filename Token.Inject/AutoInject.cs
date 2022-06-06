using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Token.Inject.tag;

namespace Token.Inject
{
    /// <summary>
    /// 注入
    /// </summary>
    public static class AutoInject
    {
        /// <summary>
        /// 主动注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="pTypes"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoInject(this IServiceCollection services, params Type[] pTypes)
        {
            List<Type> types = new List<Type>();

            //获取其他Type的程序集
            foreach(var a in pTypes.Distinct().Select(a => Assembly.GetAssembly(a)).Distinct())
            {
                types.AddRange(a.GetTypes().Where((a) => typeof(ITransientTag).IsAssignableFrom(a) || typeof(IScopedTag).IsAssignableFrom(a) || typeof(ISingletonTag).IsAssignableFrom(a)).ToList());
            }

            foreach(var serviceType in types)
            {
                var interfaces = serviceType.GetInterfaces().Where(x=>x.Name.EndsWith(serviceType.Name))?.FirstOrDefault();
                if (interfaces != null)
                {
                    if(typeof(ITransientTag).IsAssignableFrom(serviceType))
                    {
                        services.AddTransient(interfaces,serviceType);
                    }
                    else if(typeof(IScopedTag).IsAssignableFrom(serviceType))
                    {
                        services.AddScoped(interfaces,serviceType);
                    }
                    else if(typeof(ISingletonTag).IsAssignableFrom(serviceType))
                    {
                        services.AddSingleton(interfaces,serviceType);
                    }
                }
                else
                {
                    if(typeof(ITransientTag).IsAssignableFrom(serviceType))
                    {
                        services.AddTransient(serviceType);
                    }
                    else if(typeof(IScopedTag).IsAssignableFrom(serviceType))
                    {
                        services.AddScoped(serviceType);
                    }
                    else if(typeof(ISingletonTag).IsAssignableFrom(serviceType))
                    {
                        services.AddSingleton(serviceType);
                    }
                }
            }

            types.Clear();
            return services;
        }
    }
}
