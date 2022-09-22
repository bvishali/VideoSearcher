// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;
using VideoSearcherAPI.Model;
using VideoSearcherAPI.Parser;
using VideoSearcherAPI.Searcher;

public class Program
{
    public static void Main(string[] input) {
        Console.WriteLine("Hello, World!");
        QnAResponse qna = QnARetriever.Search(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "/WeeklyTechSyncUp.vtt", "is it recording?");
        if (qna != null)
        {
            List<AnswerDetails>? answerDetails = qna.Answers;
            for (int i = 0; answerDetails != null && i < answerDetails.Count; i++)
            {
                VttContentSearcher.Search(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "/WeeklyTechSyncUp.vtt", answerDetails[i]);
            }
        }
    }
}

