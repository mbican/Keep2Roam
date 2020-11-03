﻿using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace keep2roam
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var fileName = args[0];
            using var file = File.OpenRead(fileName);
            var model = await JsonSerializer.DeserializeAsync<GoogleKeepCardModel>(
                file,
                new JsonSerializerOptions{
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
            return 0;
        }
    }
}