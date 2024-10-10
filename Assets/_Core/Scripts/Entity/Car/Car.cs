using System;
using UnityEngine;

namespace Game
{
    public class Car : MonoBehaviour, IDamagable
    {
        public event Action OnTakeDamage;
        public event Action OnDie;

        public bool IsDeath => _currentHealth <= 0;

        [SerializeField] HealthView _healthView;

        [SerializeField] float _baseHealth = 100f;

        private float _currentHealth;

        private void Awake()
        {
            _currentHealth = _baseHealth;
            _healthView.Setup(this);
        }

        public HealthInfo GetHealthInfo() => new HealthInfo(_currentHealth, _baseHealth);

        public void TakeDamage(float damage)
        {
            if (IsDeath)
                return;

            _currentHealth -= damage;
            OnTakeDamage?.Invoke();

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