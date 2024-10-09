using System;
using System.Collections.Generic;

namespace Game
{
    public class MonoPool<T> where T : class, IMonoPoolable
    {
        public int ActiveElementsCount => _availableElements.Count;
        public int UnactiveElementsCount => _unavailableElements.Count;
        public int TotalElementsCount => ActiveElementsCount + UnactiveElementsCount;

        private Queue<T> _availableElements = new Queue<T>();
        private List<T> _unavailableElements = new List<T>();
        
        private Func<T> _spawnFunc;

        public MonoPool(Func<T> spawnFunc)
        {
            _spawnFunc = spawnFunc;
        }

        public T Get()
        {
            if (_availableElements.Count > 0)
            {
                T element = _availableElements.Dequeue();
                _unavailableElements.Add(element);
                return element;
            }
            else
            {
                T spawnedElement = _spawnFunc();
                spawnedElement.OnDespawn += ReturnToPool;

                _unavailableElements.Add(spawnedElement);

                return spawnedElement;
            }
        }

        public T[] GetAllActiveElements() => _unavailableElements.ToArray();

        private void ReturnToPool(IMonoPoolable element)
        {
            if (element is T poolableElement)
            {
                _availableElements.Enqueue(poolableElement);
                _unavailableElements.Remove(poolableElement);
            }
        }
    }
}