using System;
using UnityEngine;
using Zenject;

namespace Game
{
    public class ZombieBehaviour : AIBehaviour
    {
        public event Action<bool> OnFollowStateChanged;

        [SerializeField] SeparateTrigger _viewTrigger;

        private Coroutine _followingCoroutine;

        protected override void Awake()
        {
            base.Awake();

            _viewTrigger.TriggerEnter += OnViewTriggerEnter;
            _viewTrigger.TriggerExit += OnViewTriggerExit;
        }

        public void DisableBehaviour()
        {
            _viewTrigger.gameObject.SetActive(false);
            DisableMovement();

            OnFollowStateChanged?.Invoke(false);
        }

        private void OnViewTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out Car car))
            {
                if (_followingCoroutine != null)
                    StopCoroutine(_followingCoroutine);

                _followingCoroutine = StartCoroutine(Following(car.transform));
                OnFollowStateChanged?.Invoke(true);
            }
        }

        private void OnViewTriggerExit(Collider collider)
        {
            if (collider.TryGetComponent(out Car car))
            {
                if (_followingCoroutine != null)
                    StopCoroutine(_followingCoroutine);

                OnFollowStateChanged?.Invoke(false);
            }
        }
    }
}