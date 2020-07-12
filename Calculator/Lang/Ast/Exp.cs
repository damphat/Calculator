using System;

namespace Calculator.Lang.Ast {
    public abstract class Exp : Node {
        private Nullable<double> _value;
        public double Value {
            get {
                if (_value == null) {
                    _value = Eval();
                }
                return (double)_value;
            }
        }

        protected abstract double Eval();

        public override string ToString() {
            return $"{base.ToString()} = {Value})";
        }
    }
}