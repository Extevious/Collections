using System;

namespace Extevious.Collections.Generic {
    public class FixedBuffer<T> {
        private int m_offset;
        private T[] m_array;

        public T this[int index] {
            get => GetAt(index);
            set => SetAt(index, value);
        }

        public int Start { get => m_offset; }
        public int Length { get => m_array.Length; }
        public int End {
            get {
                int last = m_offset - 1;

                if (last < 0) return m_array.Length - 1;
                else return last;
            }
        }

        public FixedBuffer (int length) {
            if (length <= 0) throw new InvalidOperationException("Length must be greater-than zero.");

            m_array = new T[length];
        }

        public void Step (T element) {
            m_array[m_offset] = element;

            if (++m_offset >= m_array.Length) m_offset = 0;
        }

        public T GetAt (int index) {
            int i = m_offset + index;

            if (i >= m_array.Length) return m_array[i - m_array.Length];
            else return m_array[i];
        }

        public void SetAt (int index, T value) {
            int i = m_offset + index;

            if (i >= m_array.Length) m_array[i - m_array.Length] = value;
            else m_array[i] = value;
        }

        public T[] ToArray () {
            T[] array = new T[m_array.Length];

            for (int i = 0; i < m_array.Length; i++) array[i] = GetAt(i);

            return array;
        }
    }
}