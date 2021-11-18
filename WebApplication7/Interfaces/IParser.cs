using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Entities.Enums;

namespace WebApplication7.Interfaces
{
    public interface IParser
    {
        string SiteUrl { get; }
        Task Execute();
    }
}
