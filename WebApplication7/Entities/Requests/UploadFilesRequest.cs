using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Entities.Requests
{
    public class UploadFilesRequest
    {
        public IFormFile[] Files { get; set; }
    }
}
