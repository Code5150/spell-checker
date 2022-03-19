using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellChecker
{
    public class OpTypes
    {
        public static readonly string DEL = "DEL";
        public static readonly string INS = "INS";
    }
    class Checker
    {
        public HashSet<string> Dictionary { get; set; }

        public string Letters { get; set; } = "abcdefghijklmnopqrstuvwxyz";

        protected Func<Tuple<string, string>, IEnumerable<string>> Del;
        protected Func<Tuple<string, string>, IEnumerable<string>> Ins;

        public Checker(HashSet<string> dict)
        {
            Dictionary = dict;
            Del = s => s.Item2.Length > 0 ? new[] { $"{s.Item1}{s.Item2[1..]}" } : Array.Empty<string>();
            Ins = s => from l in Letters select $"{s.Item1}{l}{s.Item2}";
        }

        public string CheckLine(string line, char[] sep)
        {
            return line.Split(sep).Select(word => Check(word)).Aggregate((first, next) => $"{first} {next}");
        }

        public string Check(string word)
        {
            if (Dictionary.Contains(word))
            {
                return word;
            }
            var permutated = GetCorrections(Edits(word.ToLower(), Del).Union(Edits(word.ToLower(), Ins)));
            if (!String.IsNullOrEmpty(permutated))
            {
                if (permutated.Split(' ').Length == 1)
                {
                    return permutated;
                }
                return "{" + permutated + "}";
            }
            permutated = GetCorrections(Edits(word.ToLower(), Del, Ins));
            if (!String.IsNullOrEmpty(permutated))
            {
                if (permutated.Split(' ').Length == 1)
                {
                    return permutated;
                }
                return "{" + permutated + "}";
            }
            return "{" + word + "?}";
        }

        protected string GetCorrections(IEnumerable<string> permutated)
        {
            return permutated.Where(s => Dictionary.Contains(s))
                .Aggregate(String.Empty, (first, next) => $"{first} {next}").Trim();
        }

        protected HashSet<string> Edits(string word, params Func<Tuple<string, string>, IEnumerable<string>>[] permutations)
        {
            var result = new HashSet<string>() { word };
            foreach (var p in permutations)
            {
                result = new HashSet<string>(result.SelectMany(
                    w => Enumerable.Range(0, w.Length + 1).Select(i => Tuple.Create(
                        w.Substring(0, i), w.Substring(i)
                    )).SelectMany(p)
                ));
            }
            return result;
        }
    }
}
