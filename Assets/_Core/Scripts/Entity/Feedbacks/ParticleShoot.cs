using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleShoot : MonoBehaviour
    {
        [SerializeField] TurretShooter _shooter;

        private ParticleSystem _particle;

        private void Awake()
        {
            _particle = GetComponent<ParticleSystem>();

            _shooter.OnShoot += PlayParticle;
        }

        private void PlayParticle()
        {
            _particle.Play();
        }
    }
}