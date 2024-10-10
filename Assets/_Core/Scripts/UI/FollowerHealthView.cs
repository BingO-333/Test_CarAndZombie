using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(UIFollower))]
    public class FollowerHealthView : HealthView, IMonoPoolable
    {
        public event Action<IMonoPoolable> OnDespawn;

        public UIFollower Follower { get; private set; }

        private void Awake()
        {
            Follower = GetComponent<UIFollower>();
        }

        public void Despawn()
        {
            Follower.Hide(() => OnDespawn?.Invoke(this));
        }
    }
}