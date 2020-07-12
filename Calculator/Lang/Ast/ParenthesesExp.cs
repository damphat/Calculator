namespace Calculator.Lang.Ast {
    public class ParenthesesExp : Exp {
        public readonly Token Open;
        public readonly Exp Exp;
        public readonly Token Close;

        public ParenthesesExp(Token open, Exp exp, Token close) {
            Open = open;
            Exp = exp;
            Close = close;
        }

        protected override double Eval() {
            return Exp.Value;
        }

        public override string ToString() {
            return $"( exp )";
        }
    }
}