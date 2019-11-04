using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace byndyusoft_calculator
{
    class NodeDivision : INode
    {
        public int Id { get; set; }        
        public int LeftChild { get; set; }
        public int RightChild { get; set; }        
        public string UnderlyingExpression { get; set; }
        public double Value { get; set; }

        
        public double GetValue(Expression expression)
        {            
            int leftChildId = expression.Nodes[this.Id].LeftChild;
            int rightChildId = expression.Nodes[this.Id].RightChild;

            return expression.Nodes[leftChildId].GetValue(expression) /
                expression.Nodes[rightChildId].GetValue(expression);
        }
    }
}
