using SimpleXMLValidatorLibrary;

class Program
{
    static void Main(string[] args)
    {
#if DEBUG
        // You can use here to test, feel free to modify/add the test cases here.
        // You can also use other ways to test if you want.

        List<(string testCase, bool expectedResult)> testCases = new()
        {
            ("<Design><Code>hello world</Code></Design>", true),//normal case
            ("<Design><Code>hello world</Code><People>Good person</People></Design>", true),//multiple same-level tags
            ("<Design>some data<Code>hello world</Code>some more data</Design>", true),//normal case with additional valid data
            
            ("<Design><Code>hello world</Code></Design><People>", false),//no closing tag for "People" 
            ("<People><Design><Code>hello world</People></Code></Design>", false),// "/Code" should come before "/People" 
            ("<People gender=\"f\" age=\"1\">hello world</People>", true),//with attributes
            ("<People><Design><Code>hello world</Code></Design>", false),//extra opening tag
            ("<Design><Code>hello world</Code></Design></People>", false),//extra closing tag
            ("<People><Design><Code>hello world</Code><Design></People>", false),//reopening tag

            ("<People>Design><Code>hello world</Code><Design></People>", false),//synthetically erroneous input
            ("<People><Design><Code>hello world</Code<Design></People>", false),//synthetically erroneous input
            ("<People><Design><Code>hello world</Code><Design>/People>", false),//synthetically erroneous input
            ("<People><Design><Codehello world</Code><Design></People>", false),//synthetically erroneous input
            ("<People><Design><Code>", false),//synthetically erroneous input
            ("<People><Design><Code>hello world", false),//synthetically erroneous input
        };
        Console.WriteLine($"Linear XML validation algorithm:");
        int failedCount = 0;
        foreach ((string input, bool expected) in testCases)
        {
            bool result = SimpleXmlValidator.DetermineXml(input);
            string resultStr = result ? "Valid" : "Invalid";

            string mark;
            if (result == expected)
            {
                mark = "OK ";
            }
            else
            {
                mark = "NG ";
                failedCount++;
            }
            Console.WriteLine($"{mark} {input}: {resultStr}");
        }
        Console.WriteLine($"Result: {testCases.Count - failedCount}/{testCases.Count}");

        Console.WriteLine($"\nRecursive XML validation algorithm:");
        failedCount = 0;
        foreach ((string input, bool expected) in testCases)
        {
            bool result = SimpleXmlValidator.DetermineXmlRecursive(input);
            string resultStr = result ? "Valid" : "Invalid";

            string mark;
            if (result == expected)
            {
                mark = "OK ";
            }
            else
            {
                mark = "NG ";
                failedCount++;
            }
            Console.WriteLine($"{mark} {input}: {resultStr}");
        }
        Console.WriteLine($"Result: {testCases.Count - failedCount}/{testCases.Count}");
#else
        string input = args.FirstOrDefault("");
        string algorithm = "0";

        if (args.Length > 1)
            algorithm = args[1];
        
        bool result;
        if (algorithm.Equals("1"))
            result = SimpleXmlValidator.DetermineXmlRecursive(input);
        else
            result = SimpleXmlValidator.DetermineXml(input);
        Console.WriteLine(!algorithm.Equals("1") ? "Using linear algorithm:" : "Using recursive algorithm:");
        Console.WriteLine(result ? "Valid" : "Invalid");
#endif
    }
}