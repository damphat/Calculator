using Xunit;

namespace Calculator.Tests {
    public class CalcTests {
        [Fact]
        public void Unary() {
            Assert.Equal(+1, Calc.Eval("+1"));
            Assert.Equal(-1, Calc.Eval("-1"));
        }

        [Fact]
        public void Binary() {
            Assert.Equal(+5, Calc.Eval("2+3"));
            Assert.Equal(-1, Calc.Eval("2-3"));
            Assert.Equal(+6, Calc.Eval("2*3"));
            Assert.Equal(2.0/3, Calc.Eval("2/3"));
        }

        [Fact]
        public void Parentheses() {
            Assert.Equal(2, Calc.Eval("(((1)+(1)))"));
        }

        [Fact]
        public void Associativity() {
            Assert.Equal(1 - 1 - 1, Calc.Eval("1-1-1"));
        }

        [Fact]
        public void Precedence() {
            Assert.Equal(1 + 2 * 3, Calc.Eval("1+2*3"));
            Assert.Equal((1 + 2) * 3, Calc.Eval("(1+2)*3"));
            Assert.Equal(2 * 3 + 1, Calc.Eval("2*3 + 1"));
            Assert.Equal(2 * (3 + 1), Calc.Eval("2*(3 + 1)"));

        }
    }
}
