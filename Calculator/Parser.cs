using System;
using Calculator.Ast;

namespace Calculator {
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

        private Exp ParseFactor() {
            switch (t.Kind) {
                case Kind.Plus:
                case Kind.Minus: {
                    var op = t;
                    Next();
                    var ret = new UnaryExp(op, ParseFactor()); // ParseExp?
                    return ret;
                }
                case Kind.Number: {
                    var ret = new NumberExp(t);
                    Next();
                    return ret;
                }

                case Kind.Open: {
                    var open = t;
                    Next();
                    var exp = ParseExpr();
                    if (t.Kind != Kind.Close) {
                        throw new Exception("')' expected");
                    }

                    var close = t;
                    Next();
                    return new ParenthesesExp(open, exp, close);
                }

                case Kind.Eof:
                    throw new Exception("number expected, found eof");

                default:
                    throw new Exception($"number expected, found '{t.Raw}'");
            }
        }

        private Exp ParseTerm() {
            var left = ParseFactor();
            while (t.Kind == Kind.Mul || t.Kind == Kind.Div) {
                var op = t;
                Next();
                var right = ParseFactor();
                left = new BinaryExp(op, left, right);
            }

            return left;
        }

        private Exp ParseExpr() {
            var left = ParseTerm();
            while (t.Kind == Kind.Plus || t.Kind == Kind.Minus) {
                var op = t;
                Next();
                var right = ParseTerm();
                left = new BinaryExp(op, left, right);
            }

            return left;
        }


        public Exp Parse() {
            var exp = ParseExpr();
            if (t.Kind != Kind.Eof) {
                throw new Exception("eof expected");
            }

            return exp;
        }
    }
}