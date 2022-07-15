using System;
using System.Runtime.CompilerServices;

namespace Extevious.Collections.Generic {
    public class SortedList<TKey, TValue> where TKey : unmanaged {
        private TValue[] _values;
        private TKey[] _keys;
        private int _count;

        public TValue this[int index] => _values[index];

        public int Capacity {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _values.Length;
        }
        
        public int Count {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SortedList (int capacity) {
            _values = new TValue[capacity];
            _keys = new TKey[capacity];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add (TKey key, TValue item) {
            if (_count == _values.Length) {
                Array.Resize<TValue>(ref _values, _count + 1);
                Array.Resize<TKey>(ref _keys, _count + 1);
            }

            _values[_count] = item;
            _keys[_count] = key;

            _count++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool RemoveSwapBack (TKey key, out TValue item) {
            int index = IndexOf(key);

            if (index != -1) {
                item = _values[index];

                _keys[index] = _keys[_count - 1];
                _keys[_count - 1] = default;

                _values[index] = _values[_count - 1];
                _values[_count - 1] = default;

                _count--;

                return true;
            }

            item = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int IndexOf (TKey key) {
            for (int i = 0; i < _keys.Length; i++) {
                if (key.Equals(_keys[i])) return i;
            }

            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TValue[] GetValueArray () => _values;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TValue[] GetKeyArray () => _values;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear () => _count = 0;
    }
}