using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication7.Entities.Models;
using WebApplication7.Entities.Requests;
using WebApplication7.Entities.Responses;
using WebApplication7.Interfaces;
using WebApplication7.Services;
using static WebApplication7.Startup;

namespace WebApplication7.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/parser")]
    public class ParserController : Controller
    {
        private readonly ILogger<ParserController> _logger;
        private readonly ParserServiceResolver _parserServiceResolver;
        private readonly IFileParserService _fileParserService;

        public ParserController(
            ILogger<ParserController> logger,
            ParserServiceResolver parserServiceResolver,
            IFileParserService fileParserService)
        {
            _fileParserService = fileParserService;
            _parserServiceResolver = parserServiceResolver;
            _logger = logger;
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     POST /v1/parser/run-parse
        ///     {
        ///        "ParserType": "1"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">...</response>
        /// <response code="400">...</response>
        /// <response code="500">...</response>
        [SwaggerOperation(
            Summary = "RunParser",
            Description = "RunParser",
            OperationId = "RunParser"
        )]
        [HttpPost]
        [Route("run-parse")]
        [ProducesResponseType(typeof(ReportMessageResponse), 200)]
        [ProducesResponseType(typeof(ErrorMessageResponse), 400)]
        [ProducesResponseType(typeof(ErrorMessageResponse), 500)]
        public async Task<ActionResult<ReportMessageResponse>> RunParser([FromForm] RunParserRequest runParserRequest)
        {
            IParser targetParser = _parserServiceResolver(runParserRequest.ParserType);
            await targetParser.Execute();

            return new ReportMessageResponse("");
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     GET /v1/parser/compare
        ///
        /// </remarks>
        /// <response code="200">...</response>
        /// <response code="400">...</response>
        /// <response code="500">...</response>
        [SwaggerOperation(
            Summary = "Compare",
            Description = "Compare",
            OperationId = "Compare"
        )]
        [HttpPost]
        [Route("compare")]
        [ProducesResponseType(typeof(ReportMessageResponse), 200)]
        [ProducesResponseType(typeof(ErrorMessageResponse), 400)]
        [ProducesResponseType(typeof(ErrorMessageResponse), 500)]
        public async Task<ActionResult<ReportMessageResponse>> Compare([FromForm] UploadFilesRequest uploadFilesRequest)
        {
            _fileParserService.Compare(uploadFilesRequest);

            return new ReportMessageResponse("compare");
        }
    }
}
