﻿using Calculator.Ast;

namespace Calculator {
    public static class Calc {
        public static Exp Parse(string src) {
            return new Parser(src).Parse();
        }

        public static double Eval(string src) {
            return Parse(src).Value();
        }
    }
}