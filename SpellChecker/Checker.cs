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
        public HashSet<String> dictionary { get; set; }

        public string letters { get; set; } = "abcdefghijklmnopqrstuvwxyz";

        public Checker(HashSet<string> dict)
        {
            dictionary = dict;
        }

        public string check(string word)
        {
            var result = edits(
                word.ToLower(),
                s => s.Item2.Length > 0 ? new[] { $"{s.Item1}{s.Item2[1..]}" } : new string[] { },
                s => from l in letters select $"{s.Item1}{l}{s.Item2}"
            );/*.OrderBy(s => s);
            var insDelEdits = edits(
                word.ToLower(),
                s => from l in letters select $"{s.Item1}{l}{s.Item2}",
                s => s.Item2.Length > 0 ? new[] { $"{s.Item1}{s.Item2[1..]}" } : new string[] { }
            ).OrderBy(s => s);
            var result = delInsEdits.Union(insDelEdits);*/
            return result.Aggregate((first, next) => $"{first} {next}");
        }

        protected HashSet<string> edits(string word, params Func<Tuple<string, string>, IEnumerable<string>>[] transform)
        {
            var result = new HashSet<string>() { word };
            foreach (var t in transform)
            {
                result = new HashSet<string>(result.SelectMany(
                    w => Enumerable.Range(0, w.Length + 1).Select(i => Tuple.Create(
                        w.Substring(0, i), w.Substring(i)
                    )).SelectMany(t)
                ));
            }
            return result;
        }
    }
}
