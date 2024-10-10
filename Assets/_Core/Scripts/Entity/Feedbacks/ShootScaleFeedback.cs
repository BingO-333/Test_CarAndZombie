using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class ShootScaleFeedback : TweenBehaviour
    {
        [SerializeField] TurretShooter _turretShooter;

        [SerializeField] float _scale = 1.1f;
        [SerializeField] float _duration = 0.05f;

        private void Awake() => _turretShooter.OnShoot += PlayFeedback;

        private void PlayFeedback() => ScaleTo(Vector3.one * _scale, _duration).SetLoops(2, LoopType.Yoyo);
    }
}