using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Extevious.Collections.Generic {
    public class DynamicPool<T> {
        // Private Fields
        private readonly OnPoolReleaseHandler _releaseHandler;
        private readonly OnPoolEmptyHandler _emptyHandler;
        private readonly OnPoolGetHandler _getHandler;
        private readonly Queue<T> _queue;

        // Public Properties
        public bool IsEmpty {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _queue.Count == 0;
        }

        public int Count {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _queue.Count;
        }

        // Delegates
        public delegate void OnPoolReleaseHandler (T obj);
        public delegate void OnPoolDrainHandler (T obj);
        public delegate void OnPoolGetHandler (T obj);
        public delegate T OnPoolEmptyHandler ();

        #region Constructors
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DynamicPool (OnPoolEmptyHandler emptyHandler, OnPoolGetHandler getHandler = null, OnPoolReleaseHandler releaseHandler = null) {
            _queue = new Queue<T>();

            _releaseHandler = releaseHandler;
            _emptyHandler = emptyHandler;
            _getHandler = getHandler;
        }
        #endregion

        #region Public Methods
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T DynamicGet () {
            T element;

            if (!_queue.TryDequeue(out element)) element = _emptyHandler.Invoke();

            _getHandler?.Invoke(element);

            return element;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Dequeue () => _queue.Dequeue();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryDequeue (out T results) => _queue.TryDequeue(out results);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Release (T element) {
            _releaseHandler?.Invoke(element);
            _queue.Enqueue(element);
        }

        public void Drain (OnPoolDrainHandler action) {
            int count = _queue.Count;

            for (int i = 0; i < count; i++) action.Invoke(_queue.Dequeue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryPeek (out T results) => _queue.TryPeek(out results);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Queue<T> GetQueue () => _queue;
        #endregion
    }
}