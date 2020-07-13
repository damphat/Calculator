using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Calculator.Ast {
    public class NumberExp : Exp {
        public readonly Token Number;

        public NumberExp(Token number) {
            Number = number;
        }

        protected override double Eval() {
            try {
                return double.Parse(Number.Raw, CultureInfo.InvariantCulture);
            } catch (OverflowException) {
                if (Regex.IsMatch(Number.Raw, "^\\S-")) {
                    return double.NegativeInfinity;
                } else {
                    return double.PositiveInfinity;
                }
            } catch (FormatException) {
                return double.NaN;
            }
        }

        public override string ToString() {
            return Number.ToString();
        }
    }
}