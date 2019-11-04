using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace byndyusoft_calculator.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void IsCorrectWrongSymbols()
        {
            string test1 = "2+2=";
            string test2 = "&2+2";
            string test3 = "(2+2)k*2";

            var ops = new Operations();
            var parser1 = new Parser(test1, ops);
            var parser2 = new Parser(test2, ops);
            var parser3 = new Parser(test3, ops);

            bool result1 = parser1.IsCorrect(test1);
            bool result2 = parser2.IsCorrect(test2);
            bool result3 = parser3.IsCorrect(test3);

            Assert.AreEqual(false, result1);
            Assert.AreEqual(false, result2);
            Assert.AreEqual(false, result3);
        }

        [TestMethod]
        public void IsCorrectCorrect()
        {
            string test1 = "3,14*10";
            string test2 = "(2+2)*2";
            string test3 = "(2+2)*2-5";

            var ops = new Operations();
            var parser1 = new Parser(test1, ops);
            var parser2 = new Parser(test2, ops);
            var parser3 = new Parser(test3, ops);

            bool result1 = parser1.IsCorrect(test1);
            bool result2 = parser2.IsCorrect(test2);
            bool result3 = parser3.IsCorrect(test3);

            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(true, result3);
        }

        [TestMethod]
        public void IsCorrectWrongBrackets()
        {
            string test1 = "2+2)";
            string test2 = "(2+2)*2(";
            string test3 = "((2+2)*2-5";
            string test4 = "(2+2)*2)-5";
            string test5 = "(2+2)*()*2-5";

            var ops = new Operations();
            var parser1 = new Parser(test1, ops);
            var parser2 = new Parser(test2, ops);
            var parser3 = new Parser(test3, ops);
            var parser4 = new Parser(test4, ops);
            var parser5 = new Parser(test5, ops);

            bool result1 = parser1.IsCorrect(test1);
            bool result2 = parser2.IsCorrect(test2);
            bool result3 = parser3.IsCorrect(test3);
            bool result4 = parser4.IsCorrect(test4);
            bool result5 = parser5.IsCorrect(test5);

            Assert.AreEqual(false, result1);
            Assert.AreEqual(false, result2);
            Assert.AreEqual(false, result3);
            Assert.AreEqual(false, result4);
            Assert.AreEqual(false, result5);
        }

        [TestMethod]
        public void IsCorrectWrongOperators()
        {
            string test1 = "2+2-";
            string test2 = "(2+2)/*2";
            string test3 = "((2+)*2)-5";
            string test4 = "*(2+2)*2-5";
            string test5 = "(/2+2)*2-5";

            var ops = new Operations();
            var parser1 = new Parser(test1, ops);
            var parser2 = new Parser(test2, ops);
            var parser3 = new Parser(test3, ops);
            var parser4 = new Parser(test4, ops);
            var parser5 = new Parser(test5, ops);

            bool result1 = parser1.IsCorrect(test1);
            bool result2 = parser2.IsCorrect(test2);
            bool result3 = parser3.IsCorrect(test3);
            bool result4 = parser4.IsCorrect(test4);
            bool result5 = parser5.IsCorrect(test5);

            Assert.AreEqual(false, result1);
            Assert.AreEqual(false, result2);
            Assert.AreEqual(false, result3);
            Assert.AreEqual(false, result4);
            Assert.AreEqual(false, result5);
        }

        [TestMethod]
        public void IsCorrectLostOperator()
        {
            string test1 = "(2+2)(2-2)";
            string test2 = "(2+2)2";
            string test3 = "2(2+2)-5";
            string test4 = "(2+2)2-5";
            string test5 = "5+2(2+2)-5";

            var ops = new Operations();
            var parser1 = new Parser(test1, ops);
            var parser2 = new Parser(test2, ops);
            var parser3 = new Parser(test3, ops);
            var parser4 = new Parser(test4, ops);
            var parser5 = new Parser(test5, ops);

            bool result1 = parser1.IsCorrect(test1);
            bool result2 = parser2.IsCorrect(test2);
            bool result3 = parser3.IsCorrect(test3);
            bool result4 = parser4.IsCorrect(test4);
            bool result5 = parser5.IsCorrect(test5);

            Assert.AreEqual(false, result1);
            Assert.AreEqual(false, result2);
            Assert.AreEqual(false, result3);
            Assert.AreEqual(false, result4);
            Assert.AreEqual(false, result5);
        }
                
        [TestMethod]
        public void StringIsOnlyOperand()
        {
            var testString = "3,14";
            var ops = new Operations();
            var parser = new Parser(testString, ops);
            bool result = parser.CanBeDivided(testString);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void SeveralOperandsInString()
        {
            var testString = "3,14*10";
            var ops = new Operations();
            var parser = new Parser(testString, ops);
            bool result = parser.CanBeDivided(testString);
            Assert.AreEqual(true, result);
        }
        
        [TestMethod]
        public void DivideSimpleString()
        {
            var testString = "3,14*10";
            var ops = new Operations();
            var parser = new Parser(testString, ops);

            string[] result = parser.DivideIntoLexemes(testString);
            
            Assert.AreEqual("3,14", result[0]);
            Assert.AreEqual("*", result[1]);
            Assert.AreEqual("10", result[2]);
        }

        [TestMethod]
        public void DivideStringWithBrackets()
        {            
            var testString = "(3,14+10)/123";
            var ops = new Operations();
            var parser = new Parser(testString, ops);

            string[] result = parser.DivideIntoLexemes(testString);
            
            Assert.AreEqual("123", result[6]);
        }

        [TestMethod]
        public void DivideStringWithIncludedBrackets()
        {            
            var testString = "((3,14+10)/123)*7";
            var ops = new Operations();
            var parser = new Parser(testString, ops);

            string[] result = parser.DivideIntoLexemes(testString);
            
            Assert.AreEqual("7", result[10]);
        }

        [TestMethod]
        public void DivideStringBracketAtLeast()
        {            
            var testString = "3*(3-4)";
            var ops = new Operations();
            var parser = new Parser(testString, ops);

            string[] result = parser.DivideIntoLexemes(testString);
            
            Assert.AreEqual(")", result[6]);
        }

        [TestMethod]
        public void DivideStringBetweenBrackets()
        {          
            var testString = "(3-9)*(3-4)";
            var ops = new Operations();
            var parser = new Parser(testString, ops);

            string[] result = parser.DivideIntoLexemes(testString);
            
            Assert.AreEqual("*", result[5]);
        }

        [TestMethod]
        public void GetNumberAsSubstringDifferentPositions()
        {            
            var testString = "334*(345-4,56)/67";
            var ops = new Operations();
            var parser = new Parser(testString, ops);
                        
            string atTheBeginning = parser.GetNumberAsSubstring(0, testString);
            string inMiddle = parser.GetNumberAsSubstring(5, testString);
            string includeDot = parser.GetNumberAsSubstring(9, testString);
            string atTheEnd = parser.GetNumberAsSubstring(15, testString);
            
            Assert.AreEqual("334", atTheBeginning);
            Assert.AreEqual("345", inMiddle);
            Assert.AreEqual("4,56", includeDot);
            Assert.AreEqual("67", atTheEnd);
        }

        [TestMethod]
        public void GetPriorityPointSimple()
        {
            var testString = "3,14*10";
            var testArray = new string[] { "3,14", "*", "10" };
            var ops = new Operations();
            var parser = new Parser(testString, ops);
            int point = parser.GetPriorityPoint(testArray);
                        
            Assert.AreEqual(1, point);
        }

        [TestMethod]
        public void GetPriorityPointDifferent()
        {
            var testString = "3,14+10/3";
            var testArray = new string[] { "3,14", "+", "10", "/", "3" };
            var ops = new Operations();
            var parser = new Parser(testString, ops);
            int point = parser.GetPriorityPoint(testArray);

            Assert.AreEqual(1, point);
        }

        [TestMethod]
        public void GetPriorityPointBrackets()
        {
            var testString = "(3,14+10)/3";
            var testArray = new string[] 
            { "(", "3,14", "+", "10", ")", "/", "3" };

            var ops = new Operations();
            var parser = new Parser(testString, ops);
            int point = parser.GetPriorityPoint(testArray);

            Assert.AreEqual(5, point);
        }

        [TestMethod]
        public void GetPriorityPointBetweenBrackets()
        {
            var testString = "(3,14+10)/(3-5)";            
            var ops = new Operations();
            var parser = new Parser(testString, ops);

            var testArray = parser.DivideIntoLexemes(testString);
            int point = parser.GetPriorityPoint(testArray);

            Assert.AreEqual(5, point);
        }

        [TestMethod]
        public void UselessBracketsString()
        {
            string usefulBrackets = "(3,14+10)/(3-5)";
            string uselessBrackets = "((3,14+10)/(3-5))";
            var ops = new Operations();
            var parser1 = new Parser(usefulBrackets, ops);
            var parser2 = new Parser(uselessBrackets, ops);

            bool notUseless = parser1.UselessBrackets(usefulBrackets);
            bool useless = parser2.UselessBrackets(uselessBrackets);

            Assert.AreEqual(false, notUseless);
            Assert.AreEqual(true, useless);
        }

        [TestMethod]
        public void UselessBracketsArray()
        {
            string usefulBracketsStr = "(2+2)*2";
            string uselessBracketsStr = "(2+2)";

            string[] usefulBracketsArr = new string[]
            { "(", "2", "+", "2", ")", "*", "2" };

            string[] uselessBracketsArr = new string[]
            { "(", "2", "+", "2", ")" };

            var ops = new Operations();
            var parser1 = new Parser(usefulBracketsStr, ops);
            var parser2 = new Parser(uselessBracketsStr, ops);

            bool notUseless = parser1.UselessBrackets(usefulBracketsArr);
            bool useless = parser2.UselessBrackets(uselessBracketsArr);

            Assert.AreEqual(false, notUseless);
            Assert.AreEqual(true, useless);
        }
    }
}
