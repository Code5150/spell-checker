using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace SpellChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = String.Empty;
            var isEqSymbol = new Regex("\\=+");
            var dict = new HashSet<string>();
            while (!isEqSymbol.IsMatch(s))
            {
                if (!String.IsNullOrEmpty(s))
                {
                    foreach (var w in s.Split(' ')) { dict.Add(w); }
                }
                s = Console.ReadLine();
            }
            var checker = new Checker(dict);
            var linesToCorrect = new List<string>();
            s = Console.ReadLine();
            while (!isEqSymbol.IsMatch(s))
            {
                linesToCorrect.Add(s);
                s = Console.ReadLine();
            }
            if (linesToCorrect.Count > 0)
            {
                foreach (var l in linesToCorrect.Select(line => checker.CheckLine(line.ToLower(), new char[] { ' ' })))
                {
                    Console.WriteLine(l);
                }
            }
            Console.ReadLine();
        }
    }
}
