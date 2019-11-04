using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace byndyusoft_calculator
{
    class Program
    {
        static void Main()
        {                        
            Console.WriteLine("Введите выражение:");
            var input = Console.ReadLine();
            var ops = new Operations();
            var parser = new Parser(input, ops);      
            
            if (!parser.IsCorrect(parser.Input))
            {
                Console.WriteLine("Введено некорректное выражение.");
            }
            else
            {
                var expression = new Expression();
                expression.Input = parser.Input;                
                parser.Parse();
                expression.Nodes = parser.Nodes;
                var calculation = new Calculation();
                double result = calculation.Calculate(expression);
                Console.WriteLine("Результат: " + result.ToString());
            }            
            Console.WriteLine("Для выхода нажмите любую клавишу");
            Console.ReadKey();
        }
    }
}
