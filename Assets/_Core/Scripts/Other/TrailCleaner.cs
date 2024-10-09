using UnityEngine;

namespace Game
{
    public class TrailCleaner : MonoBehaviour
    {
        private TrailRenderer _trail;

        private void Awake()
        {
            _trail = GetComponent<TrailRenderer>();
        }

        private void OnEnable()
        {
            _trail.Clear();
        }
    }
}