using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace byndyusoft_calculator
{
    class NodeLeaf : INode
    {
        public int Id { get; set; }        
        public int LeftChild { get; set; }
        public int RightChild { get; set; }        
        public string UnderlyingExpression { get; set; }
        public double Value { get; set; }
        
        public double GetValue(Expression expression)
        {            
            return expression.Nodes[this.Id].Value;
        }
    }
}
