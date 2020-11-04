using System.Collections.Generic;
using CommandLine;

namespace Keep2Roam
{
    public class CommandLineArgs
    {
        [Value(0, MetaName = "Files", MetaValue = "FILE FILE2 ...", HelpText = "*.json files from Google Keep Takeout")]
        public IEnumerable<string> Files { get; set; }
    }
}