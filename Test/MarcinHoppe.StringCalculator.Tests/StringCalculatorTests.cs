using System;

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
    }
}
