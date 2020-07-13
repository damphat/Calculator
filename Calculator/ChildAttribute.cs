using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator {
    public class ChildAttribute : Attribute {
        public int Order { get; }
        public string Name { get; }

        public ChildAttribute(int order, string name) {
            this.Order = order;
            this.Name = name;
        }
    }
}
