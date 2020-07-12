using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.Tests {
    [TestClass]
    public class CalcTests {
        [TestMethod]
        public void Unary() {
            Assert.AreEqual(+1, Calc.Eval("+1"));
            Assert.AreEqual(-1, Calc.Eval("-1"));
        }

        [TestMethod]
        public void Binary() {
            Assert.AreEqual(+5, Calc.Eval("2+3"));
            Assert.AreEqual(-1, Calc.Eval("2-3"));
            Assert.AreEqual(+6, Calc.Eval("2*3"));
            Assert.AreEqual(2.0/3, Calc.Eval("2/3"));
        }

        [TestMethod]
        public void Parentheses() {
            Assert.AreEqual(2, Calc.Eval("(((1)+(1)))"));
        }

        [TestMethod]
        public void Associativity() {
            Assert.AreEqual(1 - 1 - 1, Calc.Eval("1-1-1"));
        }

        [TestMethod]
        public void Precedence() {
            Assert.AreEqual(1 + 2 * 3, Calc.Eval("1+2*3"));
            Assert.AreEqual((1 + 2) * 3, Calc.Eval("(1+2)*3"));
            Assert.AreEqual(2 * 3 + 1, Calc.Eval("2*3 + 1"));
            Assert.AreEqual(2 * (3 + 1), Calc.Eval("2*(3 + 1)"));

        }
    }
}
