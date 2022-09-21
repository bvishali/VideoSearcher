using Azure.AI.Language.QuestionAnswering;
using Azure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VideoSearcherAPI.Model;
using VideoSearcherAPI.Parser;
using System.Linq;

namespace VideoSearcherAPI.Searcher
{
    public class QnARetriever
    {
        public static QnAResponse Search(string file, string question)
        {
            QnAResponse answers = new QnAResponse();
            VttParser parser = new VttParser();
            var fileStream = File.Open(file, FileMode.Open);
            List<string> subs = parser.ConvertVTTToString(fileStream, System.Text.Encoding.ASCII);
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
            AnswersFromTextOptions options = new AnswersFromTextOptions(question, records);
            Response<AnswersFromTextResult> response = client.GetAnswersFromText(options);

            var confidentAnswers = response.Value.Answers.Where(x => x.Confidence > 0.1);

            answers.Question = question;
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

    }
}
