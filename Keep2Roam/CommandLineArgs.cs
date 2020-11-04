using System.Collections.Generic;
using CommandLine;

namespace Keep2Roam
{
    public class CommandLineArgs
    {
        [Value(0)]
        public IEnumerable<string> Files { get; set; }
    }
}