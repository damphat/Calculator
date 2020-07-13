namespace Calculator.Ast {
    public abstract class Exp : Node {
        private double? value;

        public double Value() {
            if (value == null) {
                value = Eval();
            }

            return (double) value;
        }

        protected abstract double Eval();

        public override string ToString() {
            return $"{base.ToString()} = {Value()})";
        }
    }
}