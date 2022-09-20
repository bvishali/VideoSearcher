using Azure;
using Azure.AI.Language.QuestionAnswering;
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

        public static QnAResponse Search(string file, string content)
        {
            QnAResponse answers = new QnAResponse();
            string[] frame = new string[20];
            int k=0;
            VttParser parser = new VttParser();
            var fileStream = File.Open(file, FileMode.Open);
            List<string> subs = parser.ConvertVTTToString(fileStream, System.Text.Encoding.ASCII);
            List<SubtitleItem> list = parser.ParseStream(fileStream, System.Text.Encoding.ASCII);
            fileStream.Close();
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

            fileStream.Close();

            #region QnA Service Connection and retreiving answers

            Uri endpoint = new Uri("https://hackathonlanguage.cognitiveservices.azure.com/");
            AzureKeyCredential credential = new AzureKeyCredential("b62dfbc45dbf4fae957393a7957d6299");
            string projectName = "hackathonLanguageProject";
            string deploymentName = "production";
            QuestionAnsweringClient client = new QuestionAnsweringClient(endpoint, credential);
            QuestionAnsweringProject project = new QuestionAnsweringProject(projectName, deploymentName);
            List<TextDocument> records = new List<TextDocument>();
            //records.Add(new TextDocument(Guid.NewGuid().ToString(), concatenatedText.ToString().Substring(0, 20400)));	
            foreach (var elem in subs)
            {
                records.Add(new TextDocument(Guid.NewGuid().ToString(), elem));
            }
            AnswersFromTextOptions options = new AnswersFromTextOptions("why", records);
            Response<AnswersFromTextResult> response = client.GetAnswersFromText(options);

            var confidentAnswers = response.Value.Answers.Where(x => x.Confidence > 0.1);

            answers.Question = content;
            answers.Answers = new List<string>();

            if (confidentAnswers != null && confidentAnswers.Any())
            {
                foreach (var ans in confidentAnswers)
                {
                    answers.Answers.Add(ans.Answer);
                }
            }
            else
            {
                answers.Answers.Add("Couldn't find relevant answer!");
            }


            #endregion

            return answers;
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
