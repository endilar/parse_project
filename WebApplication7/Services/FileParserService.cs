using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Entities.Models;
using WebApplication7.Entities.Requests;
using WebApplication7.Interfaces;

namespace WebApplication7.Services
{
    public class FileParserService : IFileParserService
    {
        private readonly ILogger<FileParserService> _logger;

        public FileParserService(ILogger<FileParserService> logger)
        {
            _logger = logger;
        }

        public async Task Compare(UploadFilesRequest uploadFilesRequest)
        {
            try
            {


                List<List<ComfyProduct>> filesProducts = new List<List<ComfyProduct>>();

                for (int i = 0; i < uploadFilesRequest.Files.Length; i++)
                {
                    filesProducts.Add(ReadFile(uploadFilesRequest.Files[i]));
                }

                List<ComfyProduct> minPriceProducts = new List<ComfyProduct>();

                //TOO
                foreach (ComfyProduct pr in filesProducts[0])
                {
                    ComfyProduct minPrice = pr;
                    for (int i = 1; i < filesProducts.Count; i++)
                    {
                        ComfyProduct existPr = filesProducts[i].Find(u => u.Name == pr.Name);
                        if (existPr != null)
                        {
                            minPrice = existPr.Price < minPrice.Price ? existPr : minPrice;
                        }

                    }
                    minPriceProducts.Add(minPrice);
                }

                await WriteFile(minPriceProducts);
            }
            catch (Exception ex) {
                _logger.LogError($"Error: {ex.Message}");
            }


        }

        public async Task WriteFile(List<ComfyProduct> products, string fileName = null)
        {
            List<string> text = new List<string>();
            foreach (ComfyProduct product in products)
            {
                text.Add($"{product.Name},{product.Price}");
            }

            if(fileName == null)
            {
                fileName = "min_price";
            }
             

            await File.WriteAllLinesAsync($"{fileName}.csv", text.ToArray());
        }

        private List<ComfyProduct> ReadFile(IFormFile uploadFile)
        {
            List<ComfyProduct> products = new List<ComfyProduct>();

            string line;

            System.IO.StreamReader file = new System.IO.StreamReader(uploadFile.OpenReadStream());
            while ((line = file.ReadLine()) != null)
            {
                string[] fields = line.Split(",", 4);

                products.Add(new ComfyProduct
                {
                    Name = fields[0],
                    Price = Int32.Parse(fields[1])
                });
            }
            file.Close();

            return products;
        }
    }
}
