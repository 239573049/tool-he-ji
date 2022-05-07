using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Token.HttpClientHelper
{
    public static class HttpInject
    {
        /// <summary>
        /// 注入工具
        /// </summary>
        /// <param name="services"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static IServiceCollection AddTokenHttpHelperInject(this IServiceCollection services, Uri uri)
        {
            services.AddSingleton(new TokenHttp(new HttpClient() { BaseAddress = uri }));
            return services;
        }

        /// <summary>
        /// 注入工具
        /// </summary>
        /// <param name="services"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static IServiceCollection AddTokenHttpHelperInject(this IServiceCollection services, string baseAddress)
        {
            services.AddSingleton(new TokenHttp(new HttpClient() { BaseAddress = new Uri(baseAddress) }));
            return services;
        }

        /// <summary>
        /// 注入工具
        /// </summary>
        /// <param name="services"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static IServiceCollection AddTokenHttpHelperInject(this IServiceCollection services, HttpClient client)
        {
            services.AddSingleton(new TokenHttp(client));
            return services;
        }
    }
}
