using System;
using System.Collections.Generic;
using System.Text;

namespace VideoSearcherAPI.Model
{
    public class AnswerDetails
    {
        public string? Answer { get; set; }
        public double? confidence { get; set; }
        public List<StartEndTime>? timeframe { get; set; }
    }
}
