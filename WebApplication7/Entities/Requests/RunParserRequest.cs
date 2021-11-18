using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Entities.Enums;

namespace WebApplication7.Entities.Requests
{
    /// <summary>
    /// RunParserRequest
    /// </summary>
    public class RunParserRequest
    {
        /// <summary>
        /// Select parser type
        /// </summary>
        /// <example>ParserTypeEnum</example>
        public ParserTypeEnum ParserType { get; set; }
    }
}
