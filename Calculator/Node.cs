using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Calculator {
    /// <summary>
    /// Node is a special dictionary, which is used its fields as children.
    /// </summary>
    public abstract class Node : IReadOnlyDictionary<string, Node> {
        private static readonly Dictionary<Type, Dictionary<string, FieldInfo>> TypeCaches =
            new Dictionary<Type, Dictionary<string, FieldInfo>>();

        private IReadOnlyDictionary<string, FieldInfo> FieldCaches {
            get {
                var type = GetType();
                if (TypeCaches.ContainsKey(type) == false) {
                    var fieldCaches = new Dictionary<string, FieldInfo>();
                    var fieldInfos = type.GetFields().Where(fi => typeof(Node).IsAssignableFrom(fi.FieldType));
                    foreach (var fieldInfo in fieldInfos) {
                        var name = fieldInfo.Name;
                        var att = fieldInfo.GetCustomAttribute<ChildAttribute>();
                        if (att != null) {
                            name = att.Name ?? name;
                        }
                        fieldCaches[name] = fieldInfo;
                    }

                    return TypeCaches[type] = fieldCaches;
                }

                return TypeCaches[type];
            }
        }

        public IEnumerator<KeyValuePair<string, Node>> GetEnumerator() {
            foreach (var e in FieldCaches) {
                yield return new KeyValuePair<string, Node>(e.Key, (Node) e.Value.GetValue(this));
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public int Count => FieldCaches.Count;

        public bool ContainsKey(string key) {
            return FieldCaches.ContainsKey(key);
        }

        public bool TryGetValue(string key, out Node value) {
            if (FieldCaches.TryGetValue(key, out var field)) {
                value = (Node) field.GetValue(this);
                return true;
            }

            value = default;
            return false;
        }

        public Node this[string key] {
            get {
                if (TryGetValue(key, out var value)) {
                    return value;
                }

                throw new KeyNotFoundException($"'{key}' is not found on ${GetType().Name}");
            }
        }

        public IEnumerable<string> Keys => FieldCaches.Keys;

        public IEnumerable<Node> Values {
            get {
                foreach (var e in FieldCaches.Values) {
                    yield return (Node) e.GetValue(this);
                }
            }
        }
    }
}