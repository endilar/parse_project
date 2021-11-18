using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Entities.Responses
{
    /// <summary>
    /// ErrorMessageResponse
    /// </summary>
    public class ErrorMessageResponse
    {
        public ErrorMessageResponse() 
        {
            Title = "One or more validation errors occurred.";
            Type = "https://tools.ietf.org/html/rfc7231#section-6.1";
        }

        /// <summary>
        /// Dictionary of errors ("Message": "text message ...")
        /// </summary>
        /// <example>"Message": "The email address and/or password you entered is incorrect. Please try again"</example>
        [JsonProperty("errors")]
        public Dictionary<string, string[]> Errors { get; set; }

        /// <summary>
        /// The status code of http
        /// </summary>
        /// <example>400</example>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        /// <example>One or more validation errors occurred.</example>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Trace Id
        /// </summary>
        /// <example>80000009-0001-fc00-b63f-84710c7967bb</example>
        [JsonProperty("traceId")]
        public string TraceId { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        /// <example>https://tools.ietf.org/html/rfc7231#section-6.1</example>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
