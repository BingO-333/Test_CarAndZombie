using System;
using UnityEngine;
using Zenject;

namespace Game
{
    [RequireComponent(typeof(IDamagable))]
    public class HealthViewSetuper : MonoBehaviour
    {
        private FollowerHealthView _followerHealthView;
        private UIManager _uiManager;

        private IDamagable _damagable;

        [Inject] void Construct(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        private void Awake()
        {
            _damagable = GetComponent<IDamagable>();
            _damagable.OnTakeDamage += ValidateView;
        }

        private void OnDisable()
        {
            if (_followerHealthView != null)
            {
                _followerHealthView.Despawn();
                _followerHealthView = null;
            }
        }

        private void ValidateView()
        {
            if (_followerHealthView == null)
            {
                _followerHealthView = _uiManager.GetFollowerHealthView();
                
                _followerHealthView.Setup(_damagable);
                _followerHealthView.Follower.SetFollowTarget(transform);

                _followerHealthView.Follower.Show();
            }
        }
    }
}