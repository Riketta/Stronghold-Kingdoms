namespace Kingdoms
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class Censor
    {
        public IList<string> CensoredWords;

        public Censor(IEnumerable<string> censoredWords)
        {
            if (censoredWords == null)
            {
                throw new ArgumentNullException("censoredWords");
            }
            this.CensoredWords = new List<string>(censoredWords);
        }

        public string CensorText(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
            string input = text;
            foreach (string str2 in this.CensoredWords)
            {
                string pattern = this.ToRegexPattern(str2);
                input = Regex.Replace(input, pattern, new MatchEvaluator(Censor.StarCensoredMatch), RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
            }
            return input;
        }

        private static string StarCensoredMatch(Match m)
        {
            string str = m.Captures[0].Value;
            return new string('*', str.Length);
        }

        private string ToRegexPattern(string wildcardSearch)
        {
            string str = Regex.Escape(wildcardSearch).Replace(@"\*", ".*?").Replace(@"\?", ".");
            if (str.StartsWith(".*?"))
            {
                str = str.Substring(3);
                str = @"(^\b)*?" + str;
                return (@"(^\b)*?" + str + @"\b");
            }
            return (@"\b" + str + @"\b");
        }
    }
}

