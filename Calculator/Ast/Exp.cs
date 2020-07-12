using System;

namespace Calculator.Ast {
    public abstract class Exp : Node {
        private Nullable<double> _value;
        public double Value() {
            if (_value == null) {
                _value = Eval();
            }
            return (double)_value;
        }
    

        protected abstract double Eval();

        public override string ToString() {
            return $"{base.ToString()} = {Value()})";
        }
    }
}