using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Interfaces;

namespace WebApplication7.Services
{
    public class AngleSharpManager : IAngleSharpManager
    {
        private readonly IHttpClientManager _httpClientManager;

        public AngleSharpManager(IHttpClientManager httpClientManager)
        {
            _httpClientManager = httpClientManager;
        }

        public async Task<IDocument> GetDocument(string url)
        {
            string source = await _httpClientManager.GetContentUrl(url);

            return await ConvertStringToHTMLDocument(source);
        }

        private async Task<IDocument> ConvertStringToHTMLDocument(string source, System.Func<VirtualResponse, VirtualResponse> decorateSource = null)
        {
            IConfiguration config = Configuration.Default.WithDefaultLoader();
            IBrowsingContext context = BrowsingContext.New(config);
            System.Func<VirtualResponse, VirtualResponse> decorator = decorateSource ?? ((i) => i);
            return await context.OpenAsync(req => decorator(req.Content(source)));
        }
    }
}
