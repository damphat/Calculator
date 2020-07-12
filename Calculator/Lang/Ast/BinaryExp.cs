using System;

namespace Calculator.Lang.Ast {
    public class BinaryExp : Exp {
        public readonly Exp Left;
        public readonly Token Op;
        public readonly Exp Right;

        public BinaryExp(Token op, Exp left, Exp right) {
            Op = op;
            Left = left;
            Right = right;
        }

        protected override double Eval() {
            switch (Op.Raw) {
                case "+": return Left.Value + Right.Value;
                case "-": return Left.Value - Right.Value;
                case "*": return Left.Value * Right.Value;
                case "/": return Left.Value / Right.Value;
                default:
                    throw new NotImplementedException(Op.Raw);
            }
        }

        public override string ToString() {
            return $"left {Op.Raw} right";
        }
    }
}