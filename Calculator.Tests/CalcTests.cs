using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.Tests {
    [TestClass]
    public class CalcTests {
        [TestMethod]
        public void It_works() {
            Assert.AreEqual(-9, Calc.Eval("-(1 + 2) * 3"));
        }
    }
}
