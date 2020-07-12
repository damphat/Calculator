using System;
using Xunit;

namespace Calculator.Tests {
    public class LexerTests {
        [Fact]
        public void It_reads_operators() {
            var src = " + - * / ";
            var lexer = new Lexer(src);

            var token = lexer.Read();
            Assert.Equal("+", token.Raw);
            Assert.Equal(Kind.Plus, token.Kind);

            token = lexer.Read();
            Assert.Equal("-", token.Raw);
            Assert.Equal(Kind.Minus, token.Kind);

            token = lexer.Read();
            Assert.Equal("*", token.Raw);
            Assert.Equal(Kind.Mul, token.Kind);

            token = lexer.Read();
            Assert.Equal("/", token.Raw);
            Assert.Equal(Kind.Div, token.Kind);
        }

        [Fact]
        public void It_reads_numbers() {
            var src = "0 9 01234567890";
            var lexer = new Lexer(src);

            var token = lexer.Read();
            Assert.Equal("0", token.Raw);
            Assert.Equal(Kind.Number, token.Kind);

            token = lexer.Read();
            Assert.Equal("9", token.Raw);
            Assert.Equal(Kind.Number, token.Kind);

            token = lexer.Read();
            Assert.Equal("01234567890", token.Raw);
            Assert.Equal(Kind.Number, token.Kind);
        }

        [Fact]
        public void It_reads_numbers_fraction() {
            var src = "0909.0909";
            var lexer = new Lexer(src);

            var token = lexer.Read();
            Assert.Equal("0909.0909", token.Raw);
            Assert.Equal(Kind.Number, token.Kind);
        }

        [Fact]
        public void It_reads_numbers_exponent() {
            var srcs = "1e1 1E1 1e+1 1e-1 0e0 9e9 1e0909".Split(' ');

            foreach (var src in srcs) {
                var lexer = new Lexer(src);
                var token = lexer.Read();
                Assert.Equal(src, token.Raw);
                Assert.Equal(Kind.Number, token.Kind);
            }
        }

        [Fact]
        public void It_reads_numbers_error() {
            var srcs = ".1  1.  1.1.1  1e  1e+  1e-  1e1.1".Split(new [] {' '}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var src in srcs) {
                var lexer = new Lexer(src);
                var token = lexer.Read();
                Assert.NotEqual(src, token.Raw);
            }
        }

        [Fact]
        public void It_reads_parentheses() {
            var src = " ) ( ";
            var lexer = new Lexer(src);

            var token = lexer.Read();
            Assert.Equal(")", token.Raw);
            Assert.Equal(Kind.Close, token.Kind);

            token = lexer.Read();
            Assert.Equal("(", token.Raw);
            Assert.Equal(Kind.Open, token.Kind);
        }

        [Fact]
        public void It_reads_unknown_chars() {
            var src = " ?# ";
            var lexer = new Lexer(src);

            var token = lexer.Read();
            Assert.Equal("?", token.Raw);
            Assert.Equal(Kind.Unknown, token.Kind);

            token = lexer.Read();
            Assert.Equal("#", token.Raw);
            Assert.Equal(Kind.Unknown, token.Kind);
        }

        [Fact]
        public void It_reads_eofs_at_the_end() {
            var src = "  ";
            var lexer = new Lexer(src);

            var token = lexer.Read();
            Assert.Equal("", token.Raw);
            Assert.Equal(Kind.Eof, token.Kind);

            token = lexer.Read();
            Assert.Equal("", token.Raw);
            Assert.Equal(Kind.Eof, token.Kind);
        }
    }
}