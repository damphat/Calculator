namespace Calculator.Ast {
    public class NumberExp : Exp {
        public readonly Token Number;

        public NumberExp(Token number) {
            Number = number;

        }

        protected override double Eval() {
            return double.TryParse(Number.Raw, out var ret) ? ret : double.NaN;
        }

        public override string ToString() {
            return Number.ToString();
        }
    }
}