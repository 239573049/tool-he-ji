﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Token.HttpClientHelper
{
    public class DelegaCommon
    {
        /// <summary>
        /// 响应请求拦截器（发生在请求成功后处理）
        /// </summary>
        /// <param name="message"></param>
        public delegate void ResponseMessageHandling(HttpResponseMessage message);
        /// <summary>
        /// 请求体拦截器（发生在请求前处理）
        /// </summary>
        /// <param name="message"></param>
        public delegate void RequestMessageHandling(HttpRequestMessage message);
    }
}