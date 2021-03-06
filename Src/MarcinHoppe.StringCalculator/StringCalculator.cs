﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MarcinHoppe.StringCalculator
{
    public static class StringCalculator
    {
        static readonly Regex delimiterRegex = new Regex(@"^//(\[(?<delimiter>[^\]]+)\])+$", 
                                                         RegexOptions.Multiline | RegexOptions.ExplicitCapture);

        public static int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers)) return 0;

            var delimiters = GetDelimitersOrDefault(numbers, ",").ToArray();
            var ints = RemoveDelimiter(numbers)
                        .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => int.Parse(s));

            var negativeInts = ints.Where(n => n < 0);
            if (negativeInts.Any())
                throw new NegativeNumberException(negativeInts);

            return ints.Where(n => n <= 1000).Sum();
        }

        private static string RemoveDelimiter(string numbers)
        {
            return delimiterRegex.Replace(numbers, "").Trim();
        }

        private static IEnumerable<string> GetDelimitersOrDefault(string numbers, string defaultDelimiter)
        {
            var match = delimiterRegex.Match(numbers);
            if (match.Success)
            {
                foreach (var capture in match.Groups["delimiter"].Captures.Cast<Capture>())
                {
                    yield return capture.Value;
                }
            }
            else
            {
                yield return defaultDelimiter;
            }
            yield return "\n";
        }
    }
}
