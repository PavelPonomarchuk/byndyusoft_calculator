using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace byndyusoft_calculator
{
    public class Parser
    {
        public string Input { get; set; }
        public List<INode> Nodes { get; set; }        
        private Operations Ops;

        public Parser(string input, Operations ops)
        {            
            Input = Prepare(input);
            Nodes = new List<INode>();            
            Ops = ops;
        }
        
        public void Parse()
        {
            if (!IsCorrect(Input))
                throw new ArgumentException();

            CreateNode(Input);
        }        

        public string Prepare(string input)
        {
            var result = input.Replace(" ", "");
            return result;
        }

        public bool IsCorrect(string input)
        {
            bool isCorrect = true;

            if (!CheckOperators(input) || !CheckOperatorsBetweenBrackets(input) ||
                !CheckBrackets(input) || !CheckNumbers(input) ||
                !CheckSymbols(input))
                isCorrect = false;
                
            return isCorrect;
        }

        public bool CheckOperators(string input)
        {
            bool isCorrect = true;

            for (int i = 0; i < input.Length; i++)
            {
                if (i == 0 && Ops.IsOperator(input[i]))
                {
                    isCorrect = false;
                    break;
                }

                if (i == input.Length - 1 && Ops.IsOperator(input[i]))
                    isCorrect = false;

                if (i != 0 && i != input.Length - 1)
                {
                    if (Ops.IsOperator(input[i]))
                    {
                        if (!((input[i - 1] == ')' || char.IsNumber(input[i - 1]))
                            && (input[i + 1] == '(' || char.IsNumber(input[i + 1]))))
                            isCorrect = false;                        
                    }
                }
            }            
            return isCorrect;
        }

        public bool CheckOperatorsBetweenBrackets(string input)
        {
            bool isCorrect = true;

            for (int i = 0; i < input.Length - 1; i++)
            {
                if (input[i] == ')' && input[i + 1] == '(')
                {
                    isCorrect = false;
                    break;
                }
            }            
            return isCorrect;
        }

        public bool CheckNumbers(string input)
        {
            bool isCorrect = true;
            bool wasJump = false;

            for (int i = 0; i < input.Length; i++)
            {
                if (wasJump)
                {
                    i--;
                    wasJump = false;
                }
                if (char.IsNumber(input[i]))
                {
                    string number = GetNumberAsSubstring(i, input);
                    int next = i + number.Length;                    
                    
                    if (next < input.Length)
                    {
                        if (input[next] != ')' && !Ops.IsOperator(input[next]))
                        {
                            isCorrect = false;
                            break;
                        }
                    }
                    if (i > 0)
                    {
                        if (input[i - 1] != '(' && !Ops.IsOperator(input[i - 1]))
                        {
                            isCorrect = false;
                            break;
                        }
                    }
                    i = next;
                    wasJump = true;
                    if (i == input.Length - 1)
                    {
                        i--;
                        wasJump = false;
                    }
                }
            }            
            return isCorrect;
        }

        public bool CheckSymbols(string input)
        {
            bool isCorrect = true;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != '(' && input[i] != ')' && input[i] != ',' &&
                    !Ops.IsOperator(input[i]) && !char.IsNumber(input[i]))
                {
                    isCorrect = false;
                    break;
                }
            }            
            return isCorrect;
        }

        public bool CheckBrackets(string input)
        {
            bool isCorrect = true;
            int openedBrackets = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(') openedBrackets++;
                if (input[i] == ')') openedBrackets--;
                if (openedBrackets < 0) isCorrect = false;
                if (i < input.Length - 1)
                {
                    if (input[i] == '(' && input[i + 1] == ')')
                        isCorrect = false;
                }
                if (i == input.Length - 1 && openedBrackets > 0)
                    isCorrect = false;
            }            
            return isCorrect;
        }

        public string[] DivideIntoLexemes(string input)
        {                        
            var strings = new List<string>();
            int startPointer = 0;
            bool wasJump = false;

            for (int i = 0; i < input.Length; i++)
            {
                if (wasJump)
                {
                    i--;
                    wasJump = false;
                }
                                
                if (char.IsNumber(input[i]))
                {
                    startPointer = i;
                    string numberAsSubstring = 
                        GetNumberAsSubstring(startPointer, input);
                    strings.Add(numberAsSubstring);                    
                    i = i + numberAsSubstring.Length;
                    wasJump = true;                    
                    if (i == input.Length - 1)
                    {
                        i--;
                        wasJump = false;
                    }
                }                
                else
                    strings.Add(input[i].ToString());
            }            
            return strings.ToArray();
        }

        public string GetNumberAsSubstring(int startPointer, string input)
        {            
            var builder = new StringBuilder();
            for (int i = startPointer; i < input.Length; i++)
            {
                if (!char.IsNumber(input[i]) && input[i] != ',')
                    break;
                builder.Append(input[i]);                
            }            
            return builder.ToString();
        }

        public int CreateNode(string input)
        {
            if (CanBeDivided(input))
            {
                var inputArray = DivideIntoLexemes(input);
                var indexOfPriority = GetPriorityPoint(inputArray);                                
                char op = inputArray[indexOfPriority][0];
                var currentNode = Ops.Create(op);
                Nodes.Add(currentNode);                
                int indexOfCurrent = Nodes.Count - 1;
                Nodes[indexOfCurrent].UnderlyingExpression = input;                
                Nodes[indexOfCurrent].Id = indexOfCurrent;

                string[] parts = GetParts(input);
                Nodes[indexOfCurrent].LeftChild = CreateNode(parts[0]);
                Nodes[indexOfCurrent].RightChild = CreateNode(parts[1]);
                return Nodes[indexOfCurrent].Id;
            }
            else
            {
                var currentNode = Ops.Create();
                Nodes.Add(currentNode);                
                int indexOfCurrent = Nodes.Count - 1;                
                Nodes[indexOfCurrent].Value = double.Parse(input);                
                Nodes[indexOfCurrent].Id = indexOfCurrent;
                return Nodes[indexOfCurrent].Id;
            }
        }

        public bool CanBeDivided(string input)
        {            
            foreach (var symbol in input)
                if (Ops.IsOperator(symbol)) return true;    
            
            return false;
        }

        public bool CanBeDivided(string[] inputArray)
        {            
            var builder = new StringBuilder();
            foreach (var item in inputArray)
                builder.Append(item);
            var input = builder.ToString();

            foreach (var symbol in input)
                if (Ops.IsOperator(symbol)) return true;

            return false;
        }

        public string[] GetParts(string input)
        {            
            if (UselessBrackets(input))
                throw new ArgumentException();
            if (!CanBeDivided(input))
                throw new ArgumentException();

            string[] inputArray = DivideIntoLexemes(input);                        
            int point = GetPriorityPoint(inputArray);                        
            var leftBuilder = new StringBuilder();
            var rightBuilder = new StringBuilder();

            for (int i = 0; i < point; i++)            
                leftBuilder.Append(inputArray[i]);

            for (int i = point + 1; i < inputArray.Length; i++)
                rightBuilder.Append(inputArray[i]);

            var left = leftBuilder.ToString();
            if (UselessBrackets(left))
                left = left.Substring(1, left.Length - 2);

            var right = rightBuilder.ToString();
            if (UselessBrackets(right))
                right = right.Substring(1, right.Length - 2);

            return new string[] { left, right };
        }

        public bool UselessBrackets(string input)
        {
            bool useless = true;
            int counter = 0;

            if (input[0] == '(' && input[input.Length - 1] == ')')
            {                
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] == '(') counter++;
                    if (input[i] == ')') counter--;

                    if (counter == 0 && i < input.Length - 1)
                    {
                        useless = false;
                        break;
                    }
                }
            }
            else useless = false;

            return useless;
        }

        public bool UselessBrackets(string[] inputArray)
        {            
            var builder = new StringBuilder();
            foreach (var item in inputArray)
                builder.Append(item);
            var input = builder.ToString();

            bool useless = true;
            int counter = 0;

            if (input[0] == '(' && input[input.Length - 1] == ')')
            {                
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] == '(') counter++;
                    if (input[i] == ')') counter--;

                    if (counter == 0 && i < input.Length - 1)
                    {
                        useless = false;
                        break;
                    }
                }
            }
            else useless = false;

            return useless;
        }

        public int GetPriorityPoint(string[] inputArray)
        {            
            if (UselessBrackets(inputArray))
                throw new ArgumentException();
            if (!CanBeDivided(inputArray))
                throw new ArgumentException();
            
            int pointer = 0;            
            int minPriority = Ops.Priorities.Length;
            int countOfBrackets = 0;

            for (int i = 0; i < inputArray.Length; i++)
            {                
                if (inputArray[i] == "(") countOfBrackets++;
                if (inputArray[i] == ")") countOfBrackets--;
                if (countOfBrackets > 0) continue;
                
                if (Ops.IsOperator(inputArray[i][0]))
                {
                    if (Ops.GetPriority(inputArray[i][0]) <= minPriority)
                    {
                        pointer = i;
                        minPriority = Ops.GetPriority(inputArray[i][0]);
                    }
                }
            }
            return pointer;
        }        
    }
}
