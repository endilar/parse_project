using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Interfaces
{
    public interface IAngleSharpManager
    {
        Task<IDocument> GetDocument(string url);
    }
}
