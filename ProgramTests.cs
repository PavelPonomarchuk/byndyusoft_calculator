using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace byndyusoft_calculator.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void ProgramWithoutBrackets()
        {
            var test1 = "1+2-3+1+2-3";
            var test2 = "2+2*2-2/2";
            var ops = new Operations();
            var calculation = new Calculation();

            var parser1 = new Parser(test1, ops);
            var expression1 = new Expression();
            expression1.Input = parser1.Input;
            parser1.Parse();
            expression1.Nodes = parser1.Nodes;            
            double result1 = calculation.Calculate(expression1);

            var parser2 = new Parser(test2, ops);
            var expression2 = new Expression();
            expression2.Input = parser2.Input;
            parser2.Parse();
            expression2.Nodes = parser2.Nodes;
            double result2 = calculation.Calculate(expression2);

            Assert.AreEqual(0, result1);
            Assert.AreEqual(5, result2);
        }

        [TestMethod]
        public void ProgramWithBrackets()
        {
            var test1 = "((1+2)*3)*2";
            var test2 = "(2+2)*(2+3)/2";
            var ops = new Operations();
            var calculation = new Calculation();

            var parser1 = new Parser(test1, ops);
            var expression1 = new Expression();
            expression1.Input = parser1.Input;
            parser1.Parse();
            expression1.Nodes = parser1.Nodes;
            double result1 = calculation.Calculate(expression1);

            var parser2 = new Parser(test2, ops);
            var expression2 = new Expression();
            expression2.Input = parser2.Input;
            parser2.Parse();
            expression2.Nodes = parser2.Nodes;
            double result2 = calculation.Calculate(expression2);

            Assert.AreEqual(18, result1);
            Assert.AreEqual(10, result2);
        }
    }
}
