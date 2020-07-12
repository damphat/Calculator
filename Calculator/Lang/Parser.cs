using System;
using Calculator.Lang.Ast;

namespace Calculator.Lang {
    public class Parser {
        private readonly Lexer lex;
        private Token t;

        public Parser(string src) {
            lex = new Lexer(src);
            t = lex.Read();
        }

        private void Next() {
            t = lex.Read();
        }

        private Exp ParseValue() {
            switch (t.Kind) {
                case Kind.Number: {
                    var ret = new NumberExp(t);
                    Next();
                    return ret;
                }

                case Kind.Open: {
                    var open = t;
                    Next();
                    var exp = ParseExp();
                    if (t.Kind != Kind.Close) {
                        throw new Exception("')' expected");
                    }

                    var close = t;
                    Next();
                    return new ParenthesesExp(open, exp, close);
                }

                case Kind.Eof:
                    throw new Exception("number or '(' expected");

                default:
                    throw new NotImplementedException("number or '(' expected");
            }
        }

        private Exp ParseUnary() {
            switch (t.Kind) {
                case Kind.Plus:
                case Kind.Minus: {
                    var op = t;
                    Next();
                    var ret = new UnaryExp(op, ParseUnary()); // ParseExp?
                    return ret;
                }
                default:
                    return ParseValue();
            }
        }

        private Exp ParseMulDiv() {
            var left = ParseUnary();
            if (t.Kind == Kind.Mul || t.Kind == Kind.Div) {
                var op = t;
                Next();
                var right = ParseMulDiv();
                return new BinaryExp(op, left, right);
            }

            return left;
        }

        private Exp ParsePlusMinus() {
            var left = ParseMulDiv();
            if (t.Kind == Kind.Plus || t.Kind == Kind.Minus) {
                var op = t;
                Next();
                var right = ParsePlusMinus();
                return new BinaryExp(op, left, right);
            }

            return left;
        }

        private Exp ParseExp() {
            return ParsePlusMinus();
        }

        public Exp Parse() {
            var exp = ParseExp();
            if (t.Kind != Kind.Eof) {
                throw new Exception("Eof expected");
            }

            return exp;
        }
    }
}