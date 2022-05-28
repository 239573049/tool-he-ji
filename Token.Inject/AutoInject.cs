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

            foreach(var i in types)
            {
                if(typeof(ITransientTag).IsAssignableFrom(i))
                {
                    services.AddTransient(i);
                }
                else if(typeof(IScopedTag).IsAssignableFrom(i))
                {
                    services.AddScoped(i);
                }
                else
                {
                    services.AddSingleton(i);
                }
            }

            types.Clear();
            return services;
        }
    }
}
