using System;
using System.Collections.Generic;

namespace VideoSearcherAPI.Model
{
    public class StartEndTime
    {
        public int startTimeInMiliSec { get; set; }

        public int endTimeInMiliSec { get; set;}

        public override string? ToString()
        {
            var startTs = new TimeSpan(0, 0, 0, 0, startTimeInMiliSec);
            var endTs = new TimeSpan(0, 0, 0, 0, endTimeInMiliSec);

            var res = string.Format("{0} --> {1}", startTs.ToString("G"), endTs.ToString("G"));
            return res;

        }
    }
}