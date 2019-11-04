using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace byndyusoft_calculator
{
    public class Calculation
    {
        public double Calculate(Expression expression)
        {            
            return expression.Nodes[0].GetValue(expression);
        }
    }
}
