using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(ParticleSystem))]
    public class DieParticle : MonoBehaviour
    {
        [SerializeField] GameObject _damagableGO;

        private IDamagable _damagable;
        private ParticleSystem _particle;

        private Transform _parent;
        private Vector3 _startLocalPos;

        private void OnValidate()
        {
            if (_damagableGO != null && _damagableGO.GetComponent<IDamagable>() == null)
                _damagableGO = null;
        }

        private void Awake()
        {
            _parent = transform.parent;
            _startLocalPos = transform.localPosition;

            _particle = GetComponent<ParticleSystem>();
            _damagable = _damagableGO.GetComponent<IDamagable>();

            _damagable.OnDie += PlayParticle;
        }

        private void PlayParticle()
        {
            ReturnToParent();

            transform.parent = null;
            _particle.Play();

            StopAllCoroutines();
            StartCoroutine(DelayReturnToParent());
        }

        private void ReturnToParent()
        {
            transform.parent = _parent;
            transform.localPosition = _startLocalPos;
        }

        private IEnumerator DelayReturnToParent()
        {
            yield return new WaitForSeconds(2f);
            ReturnToParent();
        }
    }
}