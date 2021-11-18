using AngleSharp.Dom;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Entities.Enums;
using WebApplication7.Entities.Models;
using WebApplication7.Entities.Requests;
using WebApplication7.Entities.Responses;
using WebApplication7.Interfaces;

namespace WebApplication7.Services
{
    public class ComfyrParserService : IParser
    {
        private readonly IFileParserService _fileParserService;
        protected readonly IAngleSharpManager _angleSharpManager;
        protected readonly ILogger<ComfyrParserService> _logger;

        public ComfyrParserService(
            IAngleSharpManager angleSharpManager,
            ILogger<ComfyrParserService> logger,
            IFileParserService fileParserService)
        {
            _angleSharpManager = angleSharpManager;
            _logger = logger;
            _fileParserService = fileParserService;
        }

        public string SiteUrl { get => "https://comfy.ua/smartfon/brand__xiaomi"; }


        public async Task Execute()
        {
            int page = 1;
            int lastPage = page;
            List<ComfyProduct> parserProducts = new List<ComfyProduct>();
            do
            {
                string productPageUrl = $"{SiteUrl}?p={page}";
                (List<ComfyProduct> parserNestedProductsFromPage, int maxCountPage) = await GetProducts(productPageUrl);
                parserProducts.AddRange(parserNestedProductsFromPage);

                lastPage = maxCountPage;
                page++;
            } while (page <= lastPage);


            _fileParserService.WriteFile(parserProducts, ParserTypeEnum.Comfy.ToString());

        }

        private async Task<(List<ComfyProduct>, int)> GetProducts(string productPageUrl)
        {
            List<ComfyProduct>  products = new List<ComfyProduct>();
            int totalCountPages = 1;

            try
            {
                IDocument document = await _angleSharpManager.GetDocument(productPageUrl);
                //string pageInfo = document.QuerySelector("div.pagi div.tot").TextContent.Trim();
                
                IHtmlCollection<IElement> productsBaseInfo = document.QuerySelectorAll("div.products-list-item");

                foreach (IElement productBaseInfo in productsBaseInfo)
                {
                    string info = productBaseInfo.QuerySelector("div.products-list-item__actions-price-current").InnerHtml.Replace("\n", "");
                    int startIndex = info.IndexOf("<span");
                    var price = info.Substring(0, startIndex).Trim().Replace(" ", "");

                    ComfyProduct product = new ComfyProduct
                    {
                        Name = productBaseInfo.QuerySelector("a.products-list-item__name").TextContent.Replace("\n", "").Trim(),
                        //Url = productBaseInfo.QuerySelector("a.products-list-item__name").GetAttribute("href").Trim(),
                        Price =  Int32.Parse(price)
                    };

                    products.Add(product);
                };
                totalCountPages = Int32.Parse(document.QuerySelectorAll("a.pagination-item").Last().PreviousElementSibling.TextContent.Trim());
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetProducts url: {productPageUrl}; \n error: {ex.Message}");
            }

            return (products, totalCountPages);
        }
    }
}
