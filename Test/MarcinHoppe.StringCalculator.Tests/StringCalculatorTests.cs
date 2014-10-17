using System;
using System.Linq;
using Xunit;

namespace MarcinHoppe.StringCalculator.Tests
{
    public class StringCalculatorTests
    {
        [Fact]
        public void Empty()
        {
            Assert.Equal(0, StringCalculator.Add(""));
        }

        [Fact]
        public void One()
        {
            Assert.Equal(1, StringCalculator.Add("1"));
        }

        [Fact]
        public void Two()
        {
            Assert.Equal(2, StringCalculator.Add("2"));
        }

        [Fact]
        public void AddOneAndTwo()
        {
            Assert.Equal(3, StringCalculator.Add("1,2"));
        }

        [Fact]
        public void AddMultipleNumbers()
        {
            var ints = Enumerable.Range(1, 10);
            var strings = string.Join(",", ints);
            Assert.Equal(ints.Sum(), StringCalculator.Add(strings));
        }

        [Fact]
        public void HandleNewlines()
        {
            Assert.Equal(6, StringCalculator.Add("1\n2,3"));
        }

        [Fact]
        public void RemoveDelimiter()
        {
            Assert.Equal("1,2,3", StringCalculator.RemoveDelimiter("//[,]\n1,2,3"));
        }

        [Fact]
        public void ExplicitDelimiter()
        {
            Assert.Equal(6, StringCalculator.Add("//[,]\n1,2,3"));
        }

        [Fact]
        public void DifferentExplicitDelimiter()
        {
            Assert.Equal(6, StringCalculator.Add("//[;]\n1;2;3"));
        }

        [Fact]
        public void ThrowsForNegativeNumbers()
        {
            var exception = Assert.Throws<NegativeNumberException>(() => StringCalculator.Add("-1"));
            Assert.Contains("-1", exception.Message);
        }

        [Fact]
        public void ExceptionContainsAllNegativeNumbers()
        {
            var exception = Assert.Throws<NegativeNumberException>(() => StringCalculator.Add("1,-1,2,-3"));
            Assert.Contains("-1", exception.Message);
            Assert.Contains("-3", exception.Message);
        }
    }
}
