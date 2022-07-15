using System;
using System.Runtime.CompilerServices;

namespace Extevious.Collections.Generic {
    public class HashList64<T> {
        private T[] _values;
        private long[] _keys;
        private int _count;

        public T this[int index] => _values[index];

        public int Capacity {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _values.Length;
        }
        public int Count {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public HashList64 () {
            _values = new T[0];
            _keys = new long[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public HashList64 (int capacity) {
            _values = new T[capacity];
            _keys = new long[capacity];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add (long key, T item) {
            if (_count == _values.Length) {
                int newCount = _count + 1;

                Array.Resize<T>(ref _values, newCount);
                Array.Resize<long>(ref _keys, newCount);
            }

            _values[_count] = item;
            _keys[_count] = key;

            _count++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Remove (long key) {
            int index = IndexOf(key);

            if (index != -1) {
                _values[index] = default;

                if (_count > 1) {
                    for (int i = index; i < _count - 1; i++) _values[i] = _values[i + 1];
                }

                _count--;

                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Remove (long key, out T item) {
            int index = IndexOf(key);

            if (index != -1) {
                item = _values[index];
                _values[index] = default;

                if (_count > 1) {
                    for (int i = index; i < _count - 1; i++) _values[i] = _values[i + 1];
                }

                _count--;

                return true;
            }

            item = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveAt (int index) {
            if (index < 0 || index >= _count) throw new IndexOutOfRangeException();

            _values[index] = default;
            _count--;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool RemoveSwapBack (long key) {
            int index = IndexOf(key);

            if (index != -1) {

                if (_count > 1) {
                    _keys[index] = _keys[_count - 1];
                    _values[index] = _values[_count - 1];
                }

                _count--;

                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool RemoveSwapBack (long key, out T item) {
            int index = IndexOf(key);

            if (index != -1) {
                item = _values[index];

                if (_count > 1) {
                    _keys[index] = _keys[_count - 1];
                    _values[index] = _values[_count - 1];
                }

                _count--;

                return true;
            }

            item = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains (long key) {
            for (int i = 0; i < _count; i++) {
                if (key == _keys[i]) return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue (long key, out T results) {
            for (int i = 0; i < _count; i++) {
                if (key == _keys[i]) {
                    results = _values[i];
                    return true;
                }
            }

            results = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int IndexOf (long key) {
            for (int i = 0; i < _count; i++) {
                if (key == _keys[i]) return i;
            }

            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Trim () {
            Array.Resize<T>(ref _values, _count);
            Array.Resize<long>(ref _keys, _count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reverse () {
            Array.Reverse<T>(_values);
            Array.Reverse<long>(_keys);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] GetValueArray () => _values;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long[] GetKeyArray () => _keys;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear () => _count = 0;
    }
}