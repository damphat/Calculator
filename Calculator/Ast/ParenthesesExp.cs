namespace Calculator.Ast {
    public class ParenthesesExp : Exp {
        [Child(1, "open")]
        public readonly Token Open;
        [Child(2, "expr")]
        public readonly Exp Exp;
        [Child(3, "close")]
        public readonly Token Close;

        public ParenthesesExp(Token open, Exp exp, Token close) {
            Open = open;
            Exp = exp;
            Close = close;
        }

        protected override double Eval() {
            return Exp.Value();
        }

        public override string ToString() {
            return $"( expr )";
        }
    }
}