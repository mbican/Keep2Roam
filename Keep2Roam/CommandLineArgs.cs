using System.Collections.Generic;
using CommandLine;

namespace Keep2Roam
{
    public class CommandLineArgs
    {
        [Option(shortName: 'i', longName: "inDir", MetaValue = "DIR", HelpText = "Input directory to read Google Keep Takeout *.json files from")]
        public string InputDirectory { get; set; }

        [Value(0, MetaName = "Files", MetaValue = "FILE [FILE2] ...", HelpText = "*.json files from Google Keep Takeout")]
        public IEnumerable<string> Files { get; set; }
    }
}