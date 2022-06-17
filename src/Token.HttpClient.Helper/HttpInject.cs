using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Token.HttpClientHelper
{
    /// <summary>
    /// 
    /// </summary>
    public static class HttpInject
    {
        /// <summary>
        /// 注入工具
        /// </summary>
        /// <param name="services"></param>
        /// <param name="uri"></param>
        /// <param name="tokenAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddTokenHttpHelperInject(this IServiceCollection services, Uri uri, Action<TokenHttp>? tokenAction = null)
        {
            var token = new TokenHttp(new HttpClient() { BaseAddress = uri });
            tokenAction?.Invoke(token);
            services.AddScoped(token.GetType());
            return services;
        }

        /// <summary>
        /// 注入工具
        /// </summary>
        /// <param name="services"></param>
        /// <param name="tokenAction"></param>
        /// <param name="baseAddress"></param>
        /// <returns></returns>
        public static IServiceCollection AddTokenHttpHelperInject(this IServiceCollection services, string baseAddress, Action<TokenHttp>? tokenAction = null)
        {
            var token = new TokenHttp(new HttpClient() { BaseAddress = new Uri(baseAddress) });
            tokenAction?.Invoke(token);
            services.AddScoped(token.GetType());
            return services;
        }

        /// <summary>
        /// 注入工具
        /// </summary>
        /// <param name="services"></param>
        /// <param name="client"></param>
        /// <param name="tokenAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddTokenHttpHelperInject(this IServiceCollection services, HttpClient client, Action<TokenHttp>? tokenAction = null)
        {
            var token = new TokenHttp(client);
            tokenAction?.Invoke(token);
            services.AddScoped(token.GetType());
            return services;
        }

    }
}
