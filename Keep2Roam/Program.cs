using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CommandLine;
using Keep2Roam.Models.GoogleKeep;

namespace Keep2Roam
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            return await Parser.Default.ParseArguments<CommandLineArgs>(args)
                .MapResult(
                    RunAndReturnExitCodeAsync,
                    _ => Task.FromResult(1));
        }

        public static async Task<int> RunAndReturnExitCodeAsync(CommandLineArgs args)
        {
            var cards = await DeserializeGoogleKeepCardsAsync(args.Files)
                .ToListAsync()
                .ConfigureAwait(false);
            return 0;
        }

        public static async IAsyncEnumerable<CardModel> DeserializeGoogleKeepCardsAsync(IEnumerable<string> fileNames)
        {
            foreach (var fileName in fileNames)
            {
                using var file = File.OpenRead(fileName);
                yield return await JsonSerializer.DeserializeAsync<CardModel>(
                    file,
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    })
                    .ConfigureAwait(false);
            }
        }
    }
}