// See https://aka.ms/new-console-template for more information
using VideoSearcherAPI.Parser;
using VideoSearcherAPI.Searcher;

public class Program
{
    public static void Main(string[] input) {
        Console.WriteLine("Hello, World!");
            
        string[] list = VttContentSearcher.Search(@"D:\Hackathon-22\VideoSearcher\VideoSearcher\VideoSearcherAPI\VideoSearcherAPI\WeeklyTechSyncUp.vtt", "So. Yeah. So this so when I said that we have a set of physical machines and the VM right on which we run the tests. Yeah. So, uh, nebulas software is, uh, nebulous.The name of that software through which we gain that access to those testing VM. OK. Uh, yeah.So whenever we have this DTP or MTP failure, the first thing that we would want to see is the repro of that failure. Now, uh, getting that repro on a local VM is almost next to impossible like I think I spent two days just to get a local repro. And it is very complicated. So the. The reason is that they like there is a test binary that is just needed to be run simply but before for that test to run. There are a lot of configurations and very complex configurations that go in the back end, so we don't have a place to like, reproduce those configurations on our local VMS and carry out the test. So that is the first level of complexity that.");
        Console.WriteLine(list);
    }
}

