using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace byndyusoft_calculator
{
    public interface INode
    {
        int Id { get; set; }        
        int LeftChild { get; set; }
        int RightChild { get; set; }        
        string UnderlyingExpression { get; set; }
        double Value { get; set; }        
                
        double GetValue(Expression expression);
    }
}
