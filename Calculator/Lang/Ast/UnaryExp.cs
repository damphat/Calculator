using System;

namespace Calculator.Lang.Ast {
    public class UnaryExp : Exp {
        public readonly Exp Exp;
        public readonly Token Op;

        public UnaryExp(Token op, Exp exp) {
            Op = op;
            Exp = exp;
        }

        protected override double Eval() {
            switch (Op.Raw) {
                case "+": return Exp.Value;
                case "-": return -Exp.Value;
                default:
                    throw new NotImplementedException(Op.Raw);
            }
        }

        public override string ToString() {
            return $"{Op.Raw} exp";
        }
    }
}