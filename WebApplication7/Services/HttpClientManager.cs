using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebApplication7.Interfaces;

namespace WebApplication7.Services
{
    public class HttpClientManager : IHttpClientManager
    {
        protected HttpClient _httpClient;
        protected ILogger<HttpClientManager> _logger;

        public HttpClientManager(ILogger<HttpClientManager> logger)
        {
            _logger = logger;
        }

        public async Task<string> GetContentUrl(string url, bool isNew = false)
        {
            var localHttpClient = GetHttpClient(isNew);
            var response = await localHttpClient.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError
                || response.StatusCode == HttpStatusCode.NotFound
                || response.StatusCode == HttpStatusCode.ServiceUnavailable
                || response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                _logger.LogError($"Response status: {response.StatusCode}");
                return null;
            }
            else
            {
                Thread.Sleep(5000);
                _logger.LogError($"Response status: {response.StatusCode}, url: {url}");
                return await GetContentUrl(url, true);
            }
        }

        private HttpClient GetHttpClient(bool isNew = false)
        {
            if (_httpClient is null || isNew)
            {
                HttpClientHandler httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                _httpClient = new HttpClient(httpClientHandler);
            }

            return _httpClient;
        }
    }
}
