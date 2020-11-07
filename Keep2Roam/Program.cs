using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CommandLine;
using Keep2Roam.Models.GoogleKeep;
using Keep2Roam.Models.RoamResearch;

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
            var cards = await DeserializeGoogleKeepCardsDirectoryAsync(args.InputDirectory)
                .Concat(DeserializeGoogleKeepCardsFilesAsync(args.Files))
                .ToListAsync()
                .ConfigureAwait(false);
            //cards.Sort((a, b) => a.UserEditedTimestampUsec.CompareTo(b.UserEditedTimestampUsec));
            var pages = new List<PageModel>();
            foreach (var card in cards)
            {
                var children = new List<NodeModel>();
                if (!string.IsNullOrEmpty(card.TextContent))
                {
                    children.Add(
                        new NodeModel
                        {
                            EditTime = card.UserEditedTimestampUsec / 1000000L,
                            String = card.TextContent,
                        });
                }
                if (0 < card.ListContent?.Count)
                {
                    foreach (var item in card.ListContent)
                    {
                        var prefix = item.IsChecked ? "{{[[DONE]]}} " : "{{[[TODO]]}} ";
                        children.Add(new NodeModel
                        {
                            EditTime = card.UserEditedTimestampUsec / 1000000L,
                            String = prefix + item.Text,
                        });
                    }
                }
                if (0 < card.Labels.Count)
                {
                    children = new List<NodeModel>(){ new NodeModel
                    {
                        Children = children,
                        EditTime = card.UserEditedTimestampUsec / 1000000L,
                        String = string.Join(
                            " ",
                            card.Labels
                                .OrderBy(a => a.Name)
                                .Select(a => "#[[" + a.Name + "]]")),
                    } };
                }
                pages.Add(new PageModel
                {
                    Title = card.Title,
                    EditTime = card.UserEditedTimestampUsec / 1000000L,
                    Children = children,
                });
            }
            using var file =
                args.OutputFileOverwrite != null
                ? new FileStream(args.OutputFileOverwrite, FileMode.Create, FileAccess.Write)
                : args.OutputFile != null
                    ? new FileStream(args.OutputFile, FileMode.CreateNew, FileAccess.Write)
                    : Console.OpenStandardOutput();
            await JsonSerializer.SerializeAsync(
                file,
                pages,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = new KebabCaseNamingPolicy(),
                    IgnoreNullValues = true,
                });
            return 0;
        }

        public static async IAsyncEnumerable<CardModel> DeserializeGoogleKeepCardsFilesAsync(IEnumerable<string> fileNames)
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

        public static IAsyncEnumerable<CardModel> DeserializeGoogleKeepCardsDirectoryAsync(string path)
        {
            return DeserializeGoogleKeepCardsFilesAsync(Directory.EnumerateFiles(path, "*.json", new EnumerationOptions { MatchCasing = MatchCasing.CaseInsensitive }));
        }
    }
}