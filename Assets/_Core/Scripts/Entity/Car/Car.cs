using System;
using UnityEngine;

namespace Game
{
    public class Car : MonoBehaviour, IDamagable
    {
        public event Action OnHealthChanged;
        public event Action OnDie;

        public bool IsDeath => _currentHealth <= 0;

        [SerializeField] float _baseHealth = 100f;

        private float _currentHealth;

        private void Awake()
        {
            _currentHealth = _baseHealth;
            OnHealthChanged?.Invoke();
        }

        public HealthInfo GetHealthInfo() => new HealthInfo(_currentHealth, _baseHealth);

        public void TakeDamage(float damage)
        {
            if (IsDeath)
                return;

            _currentHealth -= damage;
            OnHealthChanged?.Invoke();

            if (IsDeath)
                Explode();
        }

        private void Explode()
        {
            OnDie?.Invoke();
            gameObject.SetActive(false);
        }
    }
}