using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    
    public class DamageBlinkFeedback : DamageFeedback
    {
        [SerializeField] Renderer[] _renderers;

        private MaterialPropertyBlock _propertyBlock;

        private Tween _colorTween;

        protected override void Awake()
        {
            base.Awake();
            _propertyBlock = new MaterialPropertyBlock();
        }

        protected override void PlayFeedback()
        {
            _colorTween.KillIfActiveAndPlaying();
            _colorTween = DOVirtual.Color(Color.black, Color.white, 0.1f, c => {
                _propertyBlock.SetColor("_EmissionColor", c);

                foreach (var renderer in _renderers)
                    renderer.SetPropertyBlock(_propertyBlock);
            }).SetLoops(2, LoopType.Yoyo);
        }
    }
}