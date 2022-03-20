using System;
using System.Collections.Generic;
using System.Linq;

namespace SpellChecker
{
    public class Checker
    {
        public HashSet<string> Dictionary { get; set; }

        public string Letters { get; set; } = "abcdefghijklmnopqrstuvwxyz";

        protected Func<Tuple<string, string>, IEnumerable<string>> Del;
        protected Func<Tuple<string, string>, IEnumerable<string>> Ins;

        public Checker(HashSet<string> dict)
        {
            Dictionary = dict;
            Del = s => s.Item2.Length > 0 ? new[] { $"{s.Item1}{s.Item2.Substring(1)}" } : Array.Empty<string>();
            Ins = s => from l in Letters select $"{s.Item1}{l}{s.Item2}";
        }

        public string CheckLine(string line, char[] sep)
        {
            return !String.IsNullOrEmpty(line) && !String.IsNullOrWhiteSpace(line) 
                ? line.Split(sep).AsParallel().AsOrdered().Select(word => Check(word)).Aggregate((first, next) => $"{first} {next}")
                : line;
        }

        public string Check(string word)
        {
            if (Dictionary.Contains(word))
            {
                return word;
            }
            // word length == 35: 945 permutations (35 deletes and 910(35*26) inserts
            bool isParallel = Dictionary.Count >= 900 || word.Length > 34;
            var permutated = GetCorrections(Edits(word.ToLower(), Del).Union(Edits(word.ToLower(), Ins)), isParallel);
            if (!String.IsNullOrEmpty(permutated))
            {
                if (permutated.Split(' ').Length == 1)
                {
                    return permutated;
                }
                return "{" + permutated + "}";
            }
            permutated = GetCorrections(Edits(word.ToLower(), Del, Ins), isParallel);
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

        protected string GetCorrections(IEnumerable<string> permutated, bool parallel = false)
        {
            if (parallel)
            {
                return Dictionary.AsParallel().AsOrdered().Intersect(permutated.AsParallel())
                    .Aggregate(String.Empty, (first, next) => $"{first} {next}").Trim();
            }
            return Dictionary.Intersect(permutated)
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
