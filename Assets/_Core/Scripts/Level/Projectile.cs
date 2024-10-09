using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class Projectile : MonoBehaviour, IMonoPoolable
    {
        public event Action<IMonoPoolable> OnDespawn;

        [SerializeField] float _damage = 25f;
        [SerializeField] float _speed = 10f;

        private Coroutine _movingCoroutine;

        private void OnEnable()
        {
            StartCoroutine(AutoDespawn());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damagable) && damagable is not Car)
            {
                damagable.TakeDamage(_damage);
                Despawn();
            }
        }

        public void StartMove(Vector3 direction)
        {
            if (_movingCoroutine != null)
                StopCoroutine(_movingCoroutine);

            _movingCoroutine = StartCoroutine(Moving(direction.normalized));
        }

        public void Despawn()
        {
            gameObject.SetActive(false);
            OnDespawn?.Invoke(this);
        }

        private IEnumerator Moving(Vector3 direction)
        {
            while (true)
            {
                transform.position += direction * _speed * Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator AutoDespawn()
        {
            yield return new WaitForSeconds(10f);
            Despawn();
        }
    }
}