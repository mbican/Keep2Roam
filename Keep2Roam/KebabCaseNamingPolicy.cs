using System;
using System.Text;
using System.Text.Json;

namespace Keep2Roam
{
    // inspiration: https://www.rickvandenbosch.net/blog/creating-a-custom-jsonnamingpolicy/
    public class KebabCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            var result = new StringBuilder();
            for (var i = 0; i < name.Length; i++)
            {
                var c = name[i];
                if (i == 0)
                {
                    result.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    if (char.IsUpper(c))
                    {
                        result.Append('-');
                        result.Append(char.ToLowerInvariant(c));
                    }
                    else
                    {
                        result.Append(c);
                    }
                }
            }
            return result.ToString();
        }
    }
}