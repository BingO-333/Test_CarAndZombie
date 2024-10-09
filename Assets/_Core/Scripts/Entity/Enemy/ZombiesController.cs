using UnityEngine;

namespace Game
{
    public class ZombiesController : MonoBehaviour
    {
        [SerializeField] Zombie _zombiePrefab;
        [SerializeField] RPZ_Quad _spawnZone;
        [SerializeField] Car _car;
        [SerializeField] SeparateTrigger _despawnTrigger;
        [Space]
        [SerializeField] int _zombiesCount = 20;

        private Zombie[] _spawnedZombies;

        private Vector3 _zoneOffset;

        private void Awake()
        {
            _zoneOffset = _spawnZone.transform.position - _car.transform.position;
            SpawnZombies();

            _despawnTrigger.TriggerEnter += OnDespawnTriggerEnter;
        }

        private void Update()
        {
            UpdateZonePosition();
        }

        private void OnDespawnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Zombie zombie))
            {
                zombie.gameObject.SetActive(false);
                RespawnZombie(zombie);
            }
        }

        private void SpawnZombies()
        {
            Transform zombiesContainer = new GameObject("[ZombiesContainer]").transform;

            _spawnedZombies = new Zombie[_zombiesCount];

            for (int i = 0; i < _zombiesCount; i++)
            {
                Zombie spawnedZombie = 
                    Instantiate(_zombiePrefab, _spawnZone.GetRandomPointInArea(), Quaternion.identity, zombiesContainer);

                spawnedZombie.transform.Rotate(0, Random.Range(0, 360), 0);
                spawnedZombie.OnDespawn += RespawnZombie;

                _spawnedZombies[i] = spawnedZombie;
            }
        }

        private void UpdateZonePosition() => _spawnZone.transform.position = _car.transform.position + _zoneOffset;

        private void RespawnZombie(Zombie zombie)
        {
            Vector3 spawnOffset = new Vector3(Random.Range(-_spawnZone.Size.x / 2, _spawnZone.Size.x / 2), 0, _spawnZone.Size.y / 2);
            Vector3 spawnPos = _spawnZone.transform.position + spawnOffset;

            zombie.transform.position = spawnPos;
            zombie.transform.Rotate(0, Random.Range(0, 360), 0);

            zombie.gameObject.SetActive(true);
        }
    }
}