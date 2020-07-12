using System;
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
            var src = "0 9 01234567890";
            var lexer = new Lexer(src);

            var token = lexer.Read();
            Assert.AreEqual("0", token.Raw);
            Assert.AreEqual(Kind.Number, token.Kind);

            token = lexer.Read();
            Assert.AreEqual("9", token.Raw);
            Assert.AreEqual(Kind.Number, token.Kind);

            token = lexer.Read();
            Assert.AreEqual("01234567890", token.Raw);
            Assert.AreEqual(Kind.Number, token.Kind);
        }

        [TestMethod]
        public void It_reads_numbers_fraction() {
            var src = "0909.0909";
            var lexer = new Lexer(src);

            var token = lexer.Read();
            Assert.AreEqual("0909.0909", token.Raw);
            Assert.AreEqual(Kind.Number, token.Kind);
        }

        [TestMethod]
        public void It_reads_numbers_exponent() {
            var srcs = "1e1 1E1 1e+1 1e-1 0e0 9e9".Split(' ');

            foreach (var src in srcs) {
                var lexer = new Lexer(src);
                var token = lexer.Read();
                Assert.AreEqual(src, token.Raw);
                Assert.AreEqual(Kind.Number, token.Kind);
            }
        }

        [TestMethod]
        public void It_reads_numbers_error() {
            var srcs = ".1  1.  1.1.1  1e  1e+  1e-  1e1.1".Split(new [] {' '}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var src in srcs) {
                var lexer = new Lexer(src);
                var token = lexer.Read();
                Assert.AreNotEqual(src, token.Raw);
            }
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