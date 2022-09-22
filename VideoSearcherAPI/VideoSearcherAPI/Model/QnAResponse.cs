using System.Collections.Generic;

namespace VideoSearcherAPI.Model
{
    public class QnAResponse
    {
        public string? Question { get; set; }
        public List<AnswerDetails>? Answers { get; set; }
        public string? Error { get; set; }

    }
}
