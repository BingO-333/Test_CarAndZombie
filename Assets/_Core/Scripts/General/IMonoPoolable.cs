using System;

namespace Game
{
    public interface IMonoPoolable
    {
        public event Action<IMonoPoolable> OnDespawn;

        void Despawn();
    }
}