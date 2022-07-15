using System;
using System.Runtime.CompilerServices;

namespace Extevious.Collections.Generic {
    public class SimpleList<T> {
        private T[] _array;
        private int _count;

        public delegate void ElementAction (T element);

        public T this[int index] {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _array[index];
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _array[index] = value;
        }

        public int Capacity {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _array.Length;
        }
        public int Count {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SimpleList () {
            _array = new T[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SimpleList (int capacity) {
            _array = new T[capacity];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add (T item) {
            if (_count == _array.Length) Array.Resize<T>(ref _array, _count + 1);

            _array[_count++] = item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddNoResize (T item) => _array[_count++] = item;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool RemoveSwapBack (T item) {
            int index = Array.IndexOf<T>(_array, item, 0, _count);

            if (index != -1) {
                RemoveAtSwapBack(index);

                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveAtSwapBack (int index) {
            _array[index] = _array[_count - 1];
            _array[_count - 1] = default;
            _count--;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ForAll (ElementAction action) {
            for (int i = 0; i < _array.Length; i++) action.Invoke(_array[i]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] GetArray () => _array;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear () => _count = 0;
    }
}