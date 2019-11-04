using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace byndyusoft_calculator
{
    public class Operations
    {   //для поддержки новой операции модифицировать поля
        //operators, priorities и метод Create(char symbol)
        private char[] operators = { '+', '-', '*', '/' };

        public char[] Operators
        {
            get
            {
                return operators;
            }            
        }
        
        private char[][] priorities =
        {
            new char[] { '+', '-' },
            new char[] { '*', '/' }
        };

        public char[][] Priorities
        {
            get
            {
                return priorities;
            }
        }

        public bool IsOperator(char symbol)
        {            
            foreach (var item in Operators)
            {
                if (item == symbol)
                    return true;                
            }
            return false;
        }
        
        public int GetPriority(char symbol)
        {
            int priority = 0;
            for (int i = 0; i < Priorities.Length; i++)
            {
                foreach (var item in Priorities[i])
                {
                    if (item == symbol)
                    {
                        priority = i;
                        break;
                    }
                }
            }
            return priority;
        }
                
        public INode Create(char symbol)
        {            
            if (!IsOperator(symbol))
                throw new ArgumentException();

            if (symbol == '+')
                return new NodeAddition();
            else if (symbol == '-')
                return new NodeSubstraction();
            else if (symbol == '*')
                return new NodeMultiplication();
            else
                return new NodeDivision();
        }
        public INode Create()
        {
            return new NodeLeaf();
        }
    }
}
