using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoSearcherAPI.Model;
using VideoSearcherAPI.Parser;

namespace VideoSearcherAPI.Searcher
{
    public class VttContentSearcher
    {
        public static void Search(string file, AnswerDetails answerDetails)
        {
            string content = answerDetails.Answer;
            answerDetails.timeframe = new List<StartEndTime>();
            int k=0;
            VttParser parser = new VttParser();
            FileStream fileStream = File.Open(file, FileMode.Open);
            List<SubtitleItem> list = parser.ParseStream( fileStream, System.Text.Encoding.ASCII);
            fileStream.Close();

            /*search through sliding window*/
            int windowSize = 0;
            StartEndTime startEndTime = new StartEndTime();
            startEndTime.startTimeInMiliSec = 0;
            startEndTime.endTimeInMiliSec = 0;
            answerDetails.timeframe.Add(startEndTime);
            string[] contentlist = content.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < list.Count; i++) { 
                SubtitleItem item = list[i];
                for (int j = windowSize; j < contentlist.Length; j++) {
                    
                    string[] linelist = String.Join(" ", item.Lines).Split(' ');
                    if (!Exists(linelist, contentlist[j]))
                    {
                        break;
                    }
                    else { 
                        if(windowSize==0) {
                            answerDetails.timeframe[k].startTimeInMiliSec = item.StartTime;
                            answerDetails.timeframe[k].endTimeInMiliSec = item.EndTime;
                        }
                        windowSize++; 
                    }
                    if(windowSize==contentlist.Length) {
                        answerDetails.timeframe[k].startTimeInMiliSec = item.EndTime;
                        k++;
                    }
                 }
            }
        }

        private static bool Exists(string[] lList, string sCompare)
        {
            bool bVal = false;

            foreach (string s in lList)
            {
                if (s.Equals(sCompare)) { bVal = true; break; }
            }
            return bVal;
        }
    }
}
