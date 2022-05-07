using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static Token.HttpClientHelper.DelegaCommon;

namespace Token.HttpClientHelper
{
    /// <summary>
    /// 简化的Http工具
    /// </summary>
    public class TokenHttp
    {

        /// <summary>
        /// HttpClient
        /// </summary>
        public HttpClient HttpClient { get; protected set; }

        /// <summary>
        /// 设置HttpClient
        /// </summary>
        /// <param name="client"></param>
        public void SetHttpClient(HttpClient client)
        {
            HttpClient = client;
        }

        /// <summary>
        /// 响应请求拦截器
        /// </summary>
        public static ResponseMessageHandling? _responseMessage;

        /// <summary>
        /// 请求体拦截器
        /// </summary>

        public static RequestMessageHandling? _requestMessage;

        /// <summary>
        /// 请求格式
        /// </summary>
        public string MediaType { get; protected set; } = "application/json; charset=utf-8";

        /// <summary>
        /// 设置请求格式
        /// </summary>
        /// <param name="mediaType"></param>
        public void SetMediaType(string mediaType)
        {
            MediaType = mediaType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        public TokenHttp(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }


        /// <summary>
        /// 发起Get请求（异步）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<T?> GetAsync<T>(string url) where T : class
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            _requestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            _responseMessage?.Invoke(message);
            return await message.Content.ReadFromJsonAsync<T?>();
        }
        /// <summary>
        /// 发起Get请求（异步）
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> GetAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            _requestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            _responseMessage?.Invoke(message);
            return await message.Content.ReadAsStringAsync();
        }
        /// <summary>
        /// 发起Get请求（异步）
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<Stream> GetStreamAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            _requestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            _responseMessage?.Invoke(message);
            return await message.Content.ReadAsStreamAsync();
        }
        /// <summary>
        /// 发起post请求（异步）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<T?> PostAsync<T>(string url, string value) where T : class
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(value, Encoding.UTF8, MediaType);
            _requestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            _responseMessage?.Invoke(message);
            return await message.Content.ReadFromJsonAsync<T>();
        }
        /// <summary>
        /// 发起post请求（异步）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<T?> PostAsync<T>(string url, object value) where T : class
        {
            return await PostAsync<T>(url, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// 发起post请求（异步）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<string> PostAsync(string url, string value)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, MediaType)
            };
            _requestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            _responseMessage?.Invoke(message);
            return await message.Content.ReadAsStringAsync();
        }
        /// <summary>
        /// 发起post请求（异步）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<string> PostAsync(string url, object value)
        {
            return await PostAsync(url, JsonConvert.SerializeObject(value));
        }
        /// <summary>
        /// 发起Delete请求（异步）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<T> DeleteAsync<T>(string url) where T : class
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url);
            _requestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            _responseMessage?.Invoke(message);
            return await message.Content.ReadFromJsonAsync<T>();
        }
        /// <summary>
        /// 发起Delete请求（异步）
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> DeleteAsync(string url)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url);
            _requestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            _responseMessage?.Invoke(message);
            return await message.Content.ReadAsStringAsync();
        }
        /// <summary>
        /// 发起Put请求（异步）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<T> PutAsync<T>(string url, object value) where T : class
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, MediaType)
            };
            _requestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            _responseMessage?.Invoke(message);
            return await message.Content.ReadFromJsonAsync<T>();
        }
        /// <summary>
        /// 发起Put请求（异步）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<string> PutAsync(string url, object value)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, MediaType)
            };
            _requestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            _responseMessage?.Invoke(message);
            return await message.Content.ReadAsStringAsync();
        }
        /// <summary>
        /// 发起Put请求（异步）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="requestMessage"></param>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        public async Task<T> RequestMessage<T>(HttpMethod method, string url, Action<HttpRequestMessage> requestMessage, Action<HttpResponseMessage> responseMessage) where T : class
        {
            HttpRequestMessage request = new HttpRequestMessage(method, url);
            _requestMessage?.Invoke(request);
            requestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            _responseMessage?.Invoke(message);
            responseMessage?.Invoke(message);
            return await message.Content.ReadAsAsync<T>();
        }
    }
}
