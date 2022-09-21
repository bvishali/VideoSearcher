using Azure.AI.Language.QuestionAnswering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using VideoSearcherAPI.Model;
using VideoSearcherAPI.Searcher;

namespace VideoSearcherWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QnASearchController : ControllerBase
    {
        private readonly ILogger<QnASearchController> _logger;

        public QnASearchController(ILogger<QnASearchController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public QnAResponse Get(string fileName, string searchWord = "when is reward paid out?")
        {
            try
            {
                var filePath = $"{Environment.CurrentDirectory.Replace("/bin", string.Empty).Replace("/obj", string.Empty)}/Content/";
                if (string.IsNullOrWhiteSpace(fileName) || fileName == "none") fileName = "FY22 Rewards Demystification.vtt";
                return QnARetriever.Search($"{filePath}/{fileName}", searchWord);
                
            }
            catch (Exception e)
            {
                return new QnAResponse() { Error = $"{e.Message + e.StackTrace}" };
            }
        }
    }
}
