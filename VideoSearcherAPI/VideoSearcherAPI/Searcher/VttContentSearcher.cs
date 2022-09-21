using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoSearcherAPI.Parser;

namespace VideoSearcherAPI.Searcher
{
    public class VttContentSearcher
    {

        public static string[] Search(string file, string content)
        {
            string[] frame = new string[20];
            int k=0;
            VttParser parser = new VttParser();
            //List<string> subs = parser.ConvertVTTToString(File.Open(file, FileMode.Open), System.Text.Encoding.ASCII);
            List<SubtitleItem> list = parser.ParseStream(File.Open(file, FileMode.Open), System.Text.Encoding.ASCII);
            
            /*search through sliding window*/
            int windowSize = 0;
            int start=0;
            int end=0;
            string[] contentlist = content.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < list.Count; i++) { 
                SubtitleItem item = list[i];
                for (int j = windowSize; j < contentlist.Length; j++) {
                    //Console.WriteLine(item.Lines[0]+" "+contentlist[j]);
                    
                    string[] linelist = String.Join(" ", item.Lines).Split(' ');
                    if (!Exists(linelist, contentlist[j]))
                    {
                        break;
                    }
                    else { 
                        if(windowSize==0) { 
                        start = item.StartTime;
                        end = item.EndTime;
                        frame[k] = string.Join('-', start, end);
                        }
                        windowSize++; 
                    }
                    if(windowSize==contentlist.Length) {
                        end = item.EndTime;
                        frame[k++] = string.Join('-', start, end);
                        Console.WriteLine("Time"+frame);
                    }
                 }
            }
            //Console.WriteLine(subs[1]);
            return frame;
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
