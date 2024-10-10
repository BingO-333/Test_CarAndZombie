using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class Zombie : MonoBehaviour, IDamagable
    {
        public event Action<Zombie> OnDespawn;

        public event Action OnTakeDamage;
        public event Action OnDie;

        public bool IsDeath => _currentHealth <= 0;

        public ZombieBehaviour Behaviour { get; private set; }

        [SerializeField] float _baseHealth = 50f;
        [SerializeField] float _damage = 10f;

        private float _currentHealth;

        private void Awake()
        {
            Behaviour = GetComponent<ZombieBehaviour>();
        }

        private void OnEnable()
        {
            _currentHealth = _baseHealth;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Car car))
            {
                car.TakeDamage(_damage);
                Die();
            }
        }

        public HealthInfo GetHealthInfo() => new HealthInfo(_currentHealth, _baseHealth);

        public void TakeDamage(float damage)
        {
            if (IsDeath)
                return;

            _currentHealth -= damage;
            OnTakeDamage?.Invoke();

            if (IsDeath)
                Die();
        }

        private void Die()
        {
            gameObject.SetActive(false);

            OnDie?.Invoke();
            OnDespawn?.Invoke(this);
        }
    }
}