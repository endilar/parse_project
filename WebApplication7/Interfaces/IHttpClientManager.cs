using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Interfaces
{
    public interface IHttpClientManager
    {
        Task<string> GetContentUrl(string url, bool isNew = false);
    }
}
