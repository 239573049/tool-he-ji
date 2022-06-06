using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public static ResponseMessageHandling? ResponseMessage;

        /// <summary>
        /// 请求体拦截器
        /// </summary>

        public static RequestMessageHandling? RequestMessage;

        /// <summary>
        /// 请求格式
        /// </summary>
        public string ContentType { get; protected set; } = "application/json";

        /// <summary>
        /// 令牌
        /// </summary>
        public string? Token { get; protected set; }

        /// <summary>
        /// 设置请求格式
        /// </summary>
        /// <param name="contentType"></param>
        public void SetContentType(string contentType)
        {
            ContentType = contentType;
        }

        /// <summary>
        /// 设置令牌
        /// </summary>
        /// <param name="token"></param>
        public void SetToken(string token)
        {
            Token = token;
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
            if(!string.IsNullOrWhiteSpace(Token))
            {
                request.Headers.Remove("Authorization");
                request.Headers.Add("Authorization", Token);
            }
            RequestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            ResponseMessage?.Invoke(message);
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
            if(!string.IsNullOrWhiteSpace(Token))
            {
                request.Headers.Remove("Authorization");
                request.Headers.Add("Authorization", Token);
            }
            RequestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            ResponseMessage?.Invoke(message);
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
            if(!string.IsNullOrWhiteSpace(Token))
            {
                request.Headers.Remove("Authorization");
                request.Headers.Add("Authorization", Token);
            }
            RequestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            ResponseMessage?.Invoke(message);
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
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(value, Encoding.UTF8, ContentType)
            };
            if(!string.IsNullOrWhiteSpace(Token))
            {
                request.Headers.Remove("Authorization");
                request.Headers.Add("Authorization", Token);
            }
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            RequestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            ResponseMessage?.Invoke(message);
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
                Content = new StringContent(value, Encoding.UTF8, ContentType)
            };
            if(!string.IsNullOrWhiteSpace(Token))
            {
                request.Headers.Remove("Authorization");
                request.Headers.Add("Authorization", Token);
            }
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            RequestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            ResponseMessage?.Invoke(message);
            return await message.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="value"></param>
        /// <returns>Stream</returns>
        public async Task<Stream> PostStreamAsync(string url, object value)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, ContentType)
            };
            if(!string.IsNullOrWhiteSpace(Token))
            {
                request.Headers.Remove("Authorization");
                request.Headers.Add("Authorization", Token);
            }
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            RequestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            ResponseMessage?.Invoke(message);
            return await message.Content.ReadAsStreamAsync();
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
            if(!string.IsNullOrWhiteSpace(Token))
            {
                request.Headers.Remove("Authorization");
                request.Headers.Add("Authorization", Token);
            }
            RequestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            ResponseMessage?.Invoke(message);
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
            if(!string.IsNullOrWhiteSpace(Token))
            {
                request.Headers.Remove("Authorization");
                request.Headers.Add("Authorization", Token);
            }
            RequestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            ResponseMessage?.Invoke(message);
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
                Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, ContentType)
            };
            if(!string.IsNullOrWhiteSpace(Token))
            {
                request.Headers.Remove("Authorization");
                request.Headers.Add("Authorization", Token);
            }
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            RequestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            ResponseMessage?.Invoke(message);
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
                Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, ContentType)
            };
            if(!string.IsNullOrWhiteSpace(Token))
            {
                request.Headers.Remove("Authorization");
                request.Headers.Add("Authorization", Token);
            }
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            RequestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            ResponseMessage?.Invoke(message);
            return await message.Content.ReadAsStringAsync();
        }
        
        /// <summary>
        /// 通用接口（异步）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="url"></param
        /// <param name="httpContent"></param>
        /// <returns>Stream</returns>
        public async Task<Stream> RequestMessageAsync(HttpMethod method, string url,HttpContent httpContent)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, url);
            
            if(!string.IsNullOrWhiteSpace(Token))
            {
                request.Headers.Remove("Authorization");
                request.Headers.Add("Authorization", Token);
            }
            
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            request.Content = httpContent;
            
            RequestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            ResponseMessage?.Invoke(message);
            return await message.Content.ReadAsStreamAsync();
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="stream"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<T> UploadingFileAsync<T>(string url, UploadingDto uploading)
        {
            var formData = new MultipartFormDataContent
            {
                { new StreamContent(uploading.Stream), uploading.Name, uploading.FileName}
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = formData
            };

            if(!string.IsNullOrWhiteSpace(Token))
            {
                request.Headers.Remove("Authorization");
                request.Headers.Add("Authorization", Token);
            }
            RequestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            ResponseMessage?.Invoke(message);
            return await message.Content.ReadFromJsonAsync<T>();
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="files">文件流|上传名称|文件名</param>
        /// <returns></returns>
        public async Task<T> UploadingFileAsync<T>(string url, List<UploadingDto> files)
        {
            var formData = new MultipartFormDataContent();

            foreach(var f in files)
            {
                formData.Add(new StreamContent(f.Stream), f.Name, f.FileName);
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = formData
            };

            if(!string.IsNullOrWhiteSpace(Token))
            {
                request.Headers.Remove("Authorization");
                request.Headers.Add("Authorization", Token);
            }
            RequestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            ResponseMessage?.Invoke(message);
            return await message.Content.ReadFromJsonAsync<T>();
        }

        public async Task<Stream> UploadingStreamAsync(string url, List<UploadingDto> files)
        {

            var formData = new MultipartFormDataContent();

            foreach(var f in files)
            {
                formData.Add(new StreamContent(f.Stream), f.Name, f.FileName);
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = formData
            };

            if(!string.IsNullOrWhiteSpace(Token))
            {
                request.Headers.Remove("Authorization");
                request.Headers.Add("Authorization", Token);
            }
            RequestMessage?.Invoke(request);
            var message = await HttpClient.SendAsync(request);
            ResponseMessage?.Invoke(message);

            return await message.Content.ReadAsStreamAsync();
        }
    }
}
