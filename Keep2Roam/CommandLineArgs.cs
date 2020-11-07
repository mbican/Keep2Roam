using System.Collections.Generic;
using CommandLine;

namespace Keep2Roam
{
    public class CommandLineArgs
    {
        [Option(shortName: 'i', longName: "inDir", MetaValue = "DIR", HelpText = "Input directory to read Google Keep Takeout *.json files from")]
        public string InputDirectory { get; set; }

        [Option(shortName: 'o', longName: "outFil", SetName = "output")]
        public string OutputFile { get; set; }

        [Option(shortName: 'O', longName: "outFilOver", SetName = "outputOverwrite")]
        public string OutputFileOverwrite { get; set; }

        [Value(0, MetaName = "Files", MetaValue = "FILE [FILE2] ...", HelpText = "*.json files from Google Keep Takeout")]
        public IEnumerable<string> Files { get; set; }
    }
}