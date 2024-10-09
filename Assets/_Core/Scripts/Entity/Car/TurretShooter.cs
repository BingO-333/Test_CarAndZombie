using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Game
{
    public class TurretShooter : MonoBehaviour
    {
        [SerializeField] Transform _spawnPoint;
        [SerializeField] Projectile _projectilePrefab;
        [Space]
        [SerializeField] float _shootInterval = 1f;

        private MonoPool<Projectile> _bulletsPool;

        private LevelManager _levelManager;

        private Transform _bulletsContainer;

        [Inject] void Construct(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        private void Awake()
        {
            _bulletsContainer = new GameObject("[BulletsContainer]").transform;

            _bulletsPool = new MonoPool<Projectile>(SpawnProjetile);

            _levelManager.OnStateChanged += LevelStateChangedHandler;
        }

        private void LevelStateChangedHandler()
        {
            if (_levelManager.CurrentState != ELevelState.Running)
            {
                StopAllCoroutines();
                return;
            }

            StopAllCoroutines();
            StartCoroutine(Shooting());
        }

        private Projectile SpawnProjetile()
        {
            Projectile spawnedProjectile = Instantiate(_projectilePrefab, _bulletsContainer);
            spawnedProjectile.gameObject.SetActive(false);

            return spawnedProjectile;
        }

        private void Shoot()
        {
            Projectile projectile = _bulletsPool.Get();

            projectile.transform.position = _spawnPoint.position;
            projectile.transform.rotation = Quaternion.LookRotation(_spawnPoint.forward, Vector3.up);

            projectile.gameObject.SetActive(true);
            projectile.StartMove(_spawnPoint.forward);
        }

        private IEnumerator Shooting()
        {
            while (true)
            {
                yield return new WaitForSeconds(_shootInterval);
                Shoot();
            }
        }
    }
}