using System;
using System.Collections.Generic;

namespace SpellChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            var checker = new Checker(new HashSet<string>());
            Console.WriteLine(checker.check("ab"));
        }
    }
}
