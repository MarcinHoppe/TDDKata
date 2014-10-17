using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcinHoppe.StringCalculator
{
    public class NegativeNumberException : Exception
    {
        public NegativeNumberException(IEnumerable<int> negativeNumbers) 
            : base("Negative numbers: " + string.Join(",", negativeNumbers))
        { }
    }
}
