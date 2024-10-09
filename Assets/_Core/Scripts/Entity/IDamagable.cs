using System;

namespace Game
{
    public interface IDamagable
    {
        public event Action OnHealthChanged;
        public event Action OnDie;

        void TakeDamage(float damage);
        HealthInfo GetHealthInfo();
    }
}