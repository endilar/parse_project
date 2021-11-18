using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Entities.Responses
{
    /// <summary>
    /// ReportMessageResponse
    /// </summary>
    public class ReportMessageResponse
    {
        /// <summary>
        /// ReportMessageResponse constructor
        /// </summary>
        public ReportMessageResponse(string message, Dictionary<string, string> errors = null)
        {
            Message = message;
            Errors = errors ?? new Dictionary<string, string>();
        }

        /// <summary>
        /// The text message 
        /// </summary>
        /// <example>"message": "Successful email verification."</example>
        [Required]
        public string Message { get; set; }
        
        public Dictionary<string, string> Errors { get; set; }
    }
}
