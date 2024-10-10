using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class DamageScaleFeedback : DamageFeedback
    {
        [SerializeField] float _scale = 1.1f;
        [SerializeField] float _duration = 0.1f;

        private Tweener _scaleTweener;

        protected override void PlayFeedback()
        {
            _scaleTweener.KillIfActiveAndPlaying();
            _scaleTweener = transform.DOScale(Vector3.one * _scale, _duration)
                .ChangeStartValue(Vector3.one).SetLoops(2, LoopType.Yoyo);
        }
    }
}