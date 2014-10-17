using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MarcinHoppe.StringCalculator
{
    public static class StringCalculator
    {
        static readonly Regex delimiterRegex = new Regex(@"^//\[(.)\]$", RegexOptions.Multiline);

        public static int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers)) return 0;

            var delimiter = GetDelimiterOrDefault(numbers, ',');
            var ints = RemoveDelimiter(numbers).Split(delimiter, '\n')
                                               .Select(s => int.Parse(s));

            var negativeInts = ints.Where(n => n < 0);
            if (negativeInts.Any())
                throw new NegativeNumberException(negativeInts);

            return ints.Sum();
        }

        public static string RemoveDelimiter(string numbers)
        {
            return delimiterRegex.Replace(numbers, "").Trim();
        }

        private static char GetDelimiterOrDefault(string numbers, char defaultDelimiter)
        {
            var match = delimiterRegex.Match(numbers);
            return match.Success && match.Groups[1].Success
                ? match.Groups[1].Value.First()
                : defaultDelimiter;
        }
    }
}
