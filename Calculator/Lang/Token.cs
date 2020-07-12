namespace Calculator.Lang {
    public class Token : Node {
        public Token(Kind kind, string raw) {
            Kind = kind;
            Raw = raw;
        }

        public string Raw { get; }

        public Kind Kind { get; }

        public override string ToString() {
            return $"'{Raw}'";
        }
    }
}