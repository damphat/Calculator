using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.Tests {
    [TestClass]
    public class LexerTests {
        [TestMethod]
        public void It_reads_operators() {
            var src = " + - * / ";
            var lexer = new Lexer(src);

            var token = lexer.Read();
            Assert.AreEqual("+", token.Raw);
            Assert.AreEqual(Kind.Plus, token.Kind);

            token = lexer.Read();
            Assert.AreEqual("-", token.Raw);
            Assert.AreEqual(Kind.Minus, token.Kind);

            token = lexer.Read();
            Assert.AreEqual("*", token.Raw);
            Assert.AreEqual(Kind.Mul, token.Kind);

            token = lexer.Read();
            Assert.AreEqual("/", token.Raw);
            Assert.AreEqual(Kind.Div, token.Kind);
        }

        [TestMethod]
        public void It_reads_numbers() {
            var src = "0  01234567890 090.090";
            var lexer = new Lexer(src);

            var token = lexer.Read();
            Assert.AreEqual("0", token.Raw);
            Assert.AreEqual(Kind.Number, token.Kind);

            token = lexer.Read();
            Assert.AreEqual("01234567890", token.Raw);
            Assert.AreEqual(Kind.Number, token.Kind);

            token = lexer.Read();
            Assert.AreEqual("090.090", token.Raw);
            Assert.AreEqual(Kind.Number, token.Kind);
        }

        [TestMethod]
        public void It_reads_parentheses() {
            var src = " ) ( ";
            var lexer = new Lexer(src);

            var token = lexer.Read();
            Assert.AreEqual(")", token.Raw);
            Assert.AreEqual(Kind.Close, token.Kind);

            token = lexer.Read();
            Assert.AreEqual("(", token.Raw);
            Assert.AreEqual(Kind.Open, token.Kind);
        }

        [TestMethod]
        public void It_reads_unknown_chars() {
            var src = " ?# ";
            var lexer = new Lexer(src);

            var token = lexer.Read();
            Assert.AreEqual("?", token.Raw);
            Assert.AreEqual(Kind.Unknown, token.Kind);

            token = lexer.Read();
            Assert.AreEqual("#", token.Raw);
            Assert.AreEqual(Kind.Unknown, token.Kind);
        }

        [TestMethod]
        public void It_reads_eofs_at_the_end() {
            var src = "  ";
            var lexer = new Lexer(src);

            var token = lexer.Read();
            Assert.AreEqual("", token.Raw);
            Assert.AreEqual(Kind.Eof, token.Kind);

            token = lexer.Read();
            Assert.AreEqual("", token.Raw);
            Assert.AreEqual(Kind.Eof, token.Kind);
        }
    }
}