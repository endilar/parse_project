using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Entities.Models;
using WebApplication7.Entities.Requests;

namespace WebApplication7.Interfaces
{
    public interface IFileParserService
    {
        Task WriteFile(List<ComfyProduct> products, string fileName = null);

        Task Compare(UploadFilesRequest uploadFilesRequest);
    }
}
