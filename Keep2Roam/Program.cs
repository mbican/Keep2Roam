using System.IO;
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
            foreach (var fileName in args.Files)
            {
                using var file = File.OpenRead(fileName);
                var model = await JsonSerializer.DeserializeAsync<CardModel>(
                    file,
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    });
            }
            return 0;
        }
    }
}