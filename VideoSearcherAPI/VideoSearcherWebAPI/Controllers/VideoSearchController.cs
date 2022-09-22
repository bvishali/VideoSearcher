using Azure.AI.Language.QuestionAnswering;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using VideoSearcherAPI.Model;
using VideoSearcherAPI.Searcher;

namespace VideoSearcherWebAPI.Controllers
{
    [EnableCors("Policy")]
    [ApiController]
    [Route("[controller]")]
    public class VideoSearchController : ControllerBase
    {
        private readonly ILogger<QnASearchController> _logger;

        public VideoSearchController(ILogger<QnASearchController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public QnAResponse Post([FromBody] Request request)
        {
            string videoUrl = request.videoUrl;
            string keyWord = request.keyWord!=null ? request.keyWord : "when is reward paid out?";
            try
            
            {
                var fileName = "FY22 Rewards Demystification.vtt"; // Get this file from Stream APIs
                
                var filePath = $"{Environment.CurrentDirectory.Replace("/bin", string.Empty).Replace("/obj", string.Empty)}/Content/";
                if (string.IsNullOrWhiteSpace(fileName) || fileName == "none") fileName = "FY22 Rewards Demystification.vtt";
                QnAResponse qna = QnARetriever.Search($"{filePath}/{fileName}", keyWord);
                List<AnswerDetails> answerDetails = qna.Answers;
                for (int i = 0; i < answerDetails.Count; i++)
                {
                    VttContentSearcher.Search($"{filePath}/{fileName}", answerDetails[i]);
                }
                return qna;

            }
            catch (Exception e)
            {
                return new QnAResponse() { Error = $"{e.Message + e.StackTrace}" };
            }
        }
    }
}
