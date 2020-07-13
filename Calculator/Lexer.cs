namespace Calculator {
    public class Lexer {
        private const char Eof = char.MaxValue;
        private readonly string src;
        private int start;
        private int end;
        private char c;

        public Lexer(string src) {
            this.src = src;
            start = end = 0;
            c = Peek(0);
        }

        private char Peek(int i) {
            var t = end + i;
            return t < src.Length ? src[t] : Eof;
        }

        private void Next() {
            end++;
            if (end < src.Length) {
                c = src[end];
            } else {
                end = src.Length;
                c = Eof;
            }
        }

        private void Next(int i) {
            end += i;
            if (end < src.Length) {
                c = src[end];
            } else {
                end = src.Length;
                c = Eof;
            }
        }

        public Token Read() {
            while (c == ' ' || c == '\r' || c == '\n' || c == '\t') {
                Next();
            }

            start = end;
            switch (c) {
                case Eof:
                    return new Token(Kind.Eof, "");
                case '(':
                    Next();
                    return new Token(Kind.Open, "(");
                case ')':
                    Next();
                    return new Token(Kind.Close, ")");
                case '+':
                    Next();
                    return new Token(Kind.Plus, "+");
                case '-':
                    Next();
                    return new Token(Kind.Minus, "-");
                case '*':
                    Next();
                    return new Token(Kind.Mul, "*");
                case '/':
                    Next();
                    return new Token(Kind.Div, "/");
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9': {
                    Next();

                    // scan "[0-9]*"
                    while (c >= '0' && c <= '9') {
                        Next();
                    }

                    // scan "[.][0-9]+"
                    if (c == '.') {
                        var c1 = Peek(1);
                        if (c1 >= '0' && c1 <= '9') {
                            Next(2);
                        }

                        while (c >= '0' && c <= '9') {
                            Next();
                        }
                    }

                    // scan "((e|E) (+|-)? [0-9]+)?"
                    if (c == 'e' || c == 'E') {
                        var count = 1;
                        var t = Peek(1);
                        if (t == '+' || t == '-') {
                            count++;
                            t = Peek(2);
                        }

                        if (t >= '0' && t <= '9') {
                            Next(count + 1);

                            while (c >= '0' && c <= '9') {
                                Next();
                            }
                        }
                    }

                    return new Token(Kind.Number, src.Substring(start, end - start));
                }
                default:
                    // TODO scan identifier

                    // scan unknown char
                    Next();
                    return new Token(Kind.Unknown, src.Substring(start, end - start));
            }
        }
    }
}