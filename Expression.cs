using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace byndyusoft_calculator
{
    public class Expression
    {
        public string Input { get; set; }
        public List<INode> Nodes { get; set; }
    }
}
