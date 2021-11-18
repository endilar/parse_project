using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Entities.Enums;
using WebApplication7.Interfaces;

namespace WebApplication7.Services
{
    public class HotlineParserService : IParser
    {
        public HotlineParserService(
          IAngleSharpManager angleSharpManager,
          ILogger<FoxtrotParserService> _logger)
        {

        }

        public string SiteUrl => throw new NotImplementedException();

        public Task Execute()
        {
            throw new NotImplementedException();
        }
    }
}
