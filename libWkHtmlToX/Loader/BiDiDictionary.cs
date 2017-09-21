
namespace libWkHtmlToX
{


    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    public class BiDiDictionary<TFirst, TSecond> :
          System.Collections.Generic.IDictionary<TFirst, TSecond>
        , System.Collections.IDictionary
#if !NET_2_0
        , System.Collections.Generic.IReadOnlyDictionary<TFirst, TSecond>
#endif
    {
        private readonly System.Collections.Generic.IDictionary<TFirst, TSecond> _firstToSecond =
            new System.Collections.Generic.Dictionary<TFirst, TSecond>();

        private readonly System.Collections.Generic.IDictionary<TSecond, TFirst> _secondToFirst =
            new System.Collections.Generic.Dictionary<TSecond, TFirst>();

        private readonly ReverseDictionary _reverseDictionary;

        public BiDiDictionary()
        {
            _reverseDictionary = new ReverseDictionary(this);
        }

        public System.Collections.Generic.IDictionary<TSecond, TFirst> Reverse
        {
            get { return _reverseDictionary; }
        }

        public int Count
        {
            get { return _firstToSecond.Count; }
        }

        object System.Collections.ICollection.SyncRoot
        {
            get { return ((System.Collections.ICollection)_firstToSecond).SyncRoot; }
        }

        bool System.Collections.ICollection.IsSynchronized
        {
            get { return ((System.Collections.ICollection)_firstToSecond).IsSynchronized; }
        }

        bool System.Collections.IDictionary.IsFixedSize
        {
            get { return ((System.Collections.IDictionary)_firstToSecond).IsFixedSize; }
        }

        public bool IsReadOnly
        {
            get { return _firstToSecond.IsReadOnly || _secondToFirst.IsReadOnly; }
        }

        public TSecond this[TFirst key]
        {
            get { return _firstToSecond[key]; }
            set
            {
                _firstToSecond[key] = value;
                _secondToFirst[value] = key;
            }
        }

        object System.Collections.IDictionary.this[object key]
        {
            get { return ((System.Collections.IDictionary)_firstToSecond)[key]; }
            set
            {
                ((System.Collections.IDictionary)_firstToSecond)[key] = value;
                ((System.Collections.IDictionary)_secondToFirst)[value] = key;
            }
        }

        public System.Collections.Generic.ICollection<TFirst> Keys
        {
            get { return _firstToSecond.Keys; }
        }

        System.Collections.ICollection System.Collections.IDictionary.Keys
        {
            get { return ((System.Collections.IDictionary)_firstToSecond).Keys; }
        }

#if !NET_2_0
        System.Collections.Generic.IEnumerable<TFirst> System.Collections.Generic
            .IReadOnlyDictionary<TFirst, TSecond>.Keys
        {
            get
            {
                return ((System.Collections.Generic
                  .IReadOnlyDictionary<TFirst, TSecond>)_firstToSecond).Keys;
            }
        }
#endif 

        public System.Collections.Generic.ICollection<TSecond> Values
        {
            get { return _firstToSecond.Values; }
        }

        System.Collections.ICollection System.Collections.IDictionary.Values
        {
            get { return ((System.Collections.IDictionary)_firstToSecond).Values; }
        }

#if !NET_2_0
        System.Collections.Generic.IEnumerable<TSecond> System.Collections.Generic
            .IReadOnlyDictionary<TFirst, TSecond>.Values
        {
            get
            {
                return ((System.Collections.Generic
                  .IReadOnlyDictionary<TFirst, TSecond>)_firstToSecond).Values;
            }
        }
#endif

        public System.Collections.Generic.IEnumerator<System.Collections.Generic
            .KeyValuePair<TFirst, TSecond>> GetEnumerator()
        {
            return _firstToSecond.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        System.Collections.IDictionaryEnumerator System.Collections.IDictionary.GetEnumerator()
        {
            return ((System.Collections.IDictionary)_firstToSecond).GetEnumerator();
        }

        public void Add(TFirst key, TSecond value)
        {
            _firstToSecond.Add(key, value);
            _secondToFirst.Add(value, key);
        }

        void System.Collections.IDictionary.Add(object key, object value)
        {
            ((System.Collections.IDictionary)_firstToSecond).Add(key, value);
            ((System.Collections.IDictionary)_secondToFirst).Add(value, key);
        }

        void System.Collections.Generic.ICollection<System.Collections.Generic
            .KeyValuePair<TFirst, TSecond>>.Add(System.Collections.Generic
            .KeyValuePair<TFirst, TSecond> item)
        {
            _firstToSecond.Add(item);
            _secondToFirst.Add(item.Value, item.Key);
        }

        public bool ContainsKey(TFirst key)
        {
            return _firstToSecond.ContainsKey(key);
        }

        bool System.Collections.Generic.ICollection<System.Collections.Generic
            .KeyValuePair<TFirst, TSecond>>.Contains(System.Collections.Generic
            .KeyValuePair<TFirst, TSecond> item)
        {
            return _firstToSecond.Contains(item);
        }

        public bool TryGetValue(TFirst key, out TSecond value)
        {
            return _firstToSecond.TryGetValue(key, out value);
        }

        public bool Remove(TFirst key)
        {
            TSecond value;
            if (_firstToSecond.TryGetValue(key, out value))
            {
                _firstToSecond.Remove(key);
                _secondToFirst.Remove(value);
                return true;
            }
            else
                return false;
        }

        void System.Collections.IDictionary.Remove(object key)
        {
            var firstToSecond = (System.Collections.IDictionary)_firstToSecond;
            if (!firstToSecond.Contains(key))
                return;
            var value = firstToSecond[key];
            firstToSecond.Remove(key);
            ((System.Collections.IDictionary)_secondToFirst).Remove(value);
        }

        bool System.Collections.Generic.ICollection<System.Collections.Generic
            .KeyValuePair<TFirst, TSecond>>.Remove(System.Collections.Generic
            .KeyValuePair<TFirst, TSecond> item)
        {
            return _firstToSecond.Remove(item);
        }

        bool System.Collections.IDictionary.Contains(object key)
        {
            return ((System.Collections.IDictionary)_firstToSecond).Contains(key);
        }

        public void Clear()
        {
            _firstToSecond.Clear();
            _secondToFirst.Clear();
        }

        void System.Collections.Generic.ICollection<System.Collections.Generic
            .KeyValuePair<TFirst, TSecond>>.CopyTo(System.Collections.Generic
            .KeyValuePair<TFirst, TSecond>[] array, int arrayIndex)
        {
            _firstToSecond.CopyTo(array, arrayIndex);
        }

        void System.Collections.ICollection.CopyTo(System.Array array, int index)
        {
            ((System.Collections.IDictionary)_firstToSecond).CopyTo(array, index);
        }


        private class ReverseDictionary : System.Collections.Generic.IDictionary<TSecond, TFirst>
            , System.Collections.IDictionary
#if !NET_2_0
            , System.Collections.Generic.IReadOnlyDictionary<TSecond, TFirst>
#endif
        {
            private readonly BiDiDictionary<TFirst, TSecond> _owner;

            public ReverseDictionary(BiDiDictionary<TFirst, TSecond> owner)
            {
                _owner = owner;
            }

            public int Count
            {
                get { return _owner._secondToFirst.Count; }
            }

            object System.Collections.ICollection.SyncRoot
            {
                get { return ((System.Collections.ICollection)_owner._secondToFirst).SyncRoot; }
            }

            bool System.Collections.ICollection.IsSynchronized
            {
                get { return ((System.Collections.ICollection)_owner._secondToFirst).IsSynchronized; }
            }

            bool System.Collections.IDictionary.IsFixedSize
            {
                get { return ((System.Collections.IDictionary)_owner._secondToFirst).IsFixedSize; }
            }

            public bool IsReadOnly
            {
                get { return _owner._secondToFirst.IsReadOnly || _owner._firstToSecond.IsReadOnly; }
            }

            public TFirst this[TSecond key]
            {
                get { return _owner._secondToFirst[key]; }
                set
                {
                    _owner._secondToFirst[key] = value;
                    _owner._firstToSecond[value] = key;
                }
            }

            object System.Collections.IDictionary.this[object key]
            {
                get { return ((System.Collections.IDictionary)_owner._secondToFirst)[key]; }
                set
                {
                    ((System.Collections.IDictionary)_owner._secondToFirst)[key] = value;
                    ((System.Collections.IDictionary)_owner._firstToSecond)[value] = key;
                }
            }

            public System.Collections.Generic.ICollection<TSecond> Keys
            {
                get { return _owner._secondToFirst.Keys; }
            }

            System.Collections.ICollection System.Collections.IDictionary.Keys
            {
                get { return ((System.Collections.IDictionary)_owner._secondToFirst).Keys; }
            }

#if !NET_2_0
            System.Collections.Generic.IEnumerable<TSecond> System.Collections.Generic
                .IReadOnlyDictionary<TSecond, TFirst>.Keys
            {
                get
                {
                    return ((System.Collections.Generic
                      .IReadOnlyDictionary<TSecond, TFirst>)_owner._secondToFirst).Keys;
                }
            }
#endif

            public System.Collections.Generic.ICollection<TFirst> Values
            {
                get { return _owner._secondToFirst.Values; }
            }

            System.Collections.ICollection System.Collections.IDictionary.Values
            {
                get { return ((System.Collections.IDictionary)_owner._secondToFirst).Values; }
            }

#if !NET_2_0

            System.Collections.Generic.IEnumerable<TFirst> System.Collections.Generic
                .IReadOnlyDictionary<TSecond, TFirst>.Values
            {
                get
                {
                    return ((System.Collections.Generic
                      .IReadOnlyDictionary<TSecond, TFirst>)_owner._secondToFirst).Values;
                }
            }

#endif

            public System.Collections.Generic.IEnumerator<System.Collections.Generic
                .KeyValuePair<TSecond, TFirst>> GetEnumerator()
            {
                return _owner._secondToFirst.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            System.Collections.IDictionaryEnumerator System.Collections.IDictionary.GetEnumerator()
            {
                return ((System.Collections.IDictionary)_owner._secondToFirst).GetEnumerator();
            }

            public void Add(TSecond key, TFirst value)
            {
                _owner._secondToFirst.Add(key, value);
                _owner._firstToSecond.Add(value, key);
            }

            void System.Collections.IDictionary.Add(object key, object value)
            {
                ((System.Collections.IDictionary)_owner._secondToFirst).Add(key, value);
                ((System.Collections.IDictionary)_owner._firstToSecond).Add(value, key);
            }

            void System.Collections.Generic.ICollection<System.Collections.Generic
                .KeyValuePair<TSecond, TFirst>>.Add(System.Collections.Generic
                .KeyValuePair<TSecond, TFirst> item)
            {
                _owner._secondToFirst.Add(item);
                _owner._firstToSecond.Add(item.Value, item.Key);
            }

            public bool ContainsKey(TSecond key)
            {
                return _owner._secondToFirst.ContainsKey(key);
            }

            bool System.Collections.Generic.ICollection<System.Collections.Generic
                .KeyValuePair<TSecond, TFirst>>.Contains(System.Collections.Generic
                .KeyValuePair<TSecond, TFirst> item)
            {
                return _owner._secondToFirst.Contains(item);
            }

            public bool TryGetValue(TSecond key, out TFirst value)
            {
                return _owner._secondToFirst.TryGetValue(key, out value);
            }

            public bool Remove(TSecond key)
            {
                TFirst value;
                if (_owner._secondToFirst.TryGetValue(key, out value))
                {
                    _owner._secondToFirst.Remove(key);
                    _owner._firstToSecond.Remove(value);
                    return true;
                }
                else
                    return false;
            }

            void System.Collections.IDictionary.Remove(object key)
            {
                var firstToSecond = (System.Collections.IDictionary)_owner._secondToFirst;
                if (!firstToSecond.Contains(key))
                    return;
                var value = firstToSecond[key];
                firstToSecond.Remove(key);
                ((System.Collections.IDictionary)_owner._firstToSecond).Remove(value);
            }

            bool System.Collections.Generic.ICollection<System.Collections.Generic
                .KeyValuePair<TSecond, TFirst>>.Remove(System.Collections.Generic
                .KeyValuePair<TSecond, TFirst> item)
            {
                return _owner._secondToFirst.Remove(item);
            }

            bool System.Collections.IDictionary.Contains(object key)
            {
                return ((System.Collections.IDictionary)_owner._secondToFirst).Contains(key);
            }

            public void Clear()
            {
                _owner._secondToFirst.Clear();
                _owner._firstToSecond.Clear();
            }

            void System.Collections.Generic.ICollection<System.Collections.Generic
                .KeyValuePair<TSecond, TFirst>>.CopyTo(System.Collections.Generic
                .KeyValuePair<TSecond, TFirst>[] array, int arrayIndex)
            {
                _owner._secondToFirst.CopyTo(array, arrayIndex);
            }

            void System.Collections.ICollection.CopyTo(System.Array array, int index)
            {
                ((System.Collections.IDictionary)_owner._secondToFirst).CopyTo(array, index);
            }


        } // End Class ReverseDictionary 


    } // End Class BiDictionary<TFirst, TSecond>


} // End namespace libWkHtmlToX
