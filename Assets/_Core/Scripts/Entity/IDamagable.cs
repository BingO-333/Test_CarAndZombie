using System;

namespace Game
{
    public interface IDamagable
    {
        public event Action OnTakeDamage;
        public event Action OnDie;

        void TakeDamage(float damage);
        HealthInfo GetHealthInfo();
    }
}