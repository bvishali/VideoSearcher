using System;
using System.Collections.Generic;
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
            VttParser parser = new VttParser();
            List<string> subs = parser.ConvertVTTToString(File.Open(file, FileMode.Open), System.Text.Encoding.ASCII);
            /*search through sliding window*/
            //int windowSize = 0;
            //string[] contentlist = content.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //for (int i = 0; i < list.Count; i++) { 
            //    SubtitleItem item = list[i];
            //    for (int j = windowSize; j < contentlist.Length; j++) {
            //        if (!item.Lines.Contains(contentlist[j]))
            //        {
            //            break;
            //        }
            //        else { windowSize++; }
            //    }
            //}
            //Console.WriteLine(subs[1]);
            return frame;
        }
    }
}
