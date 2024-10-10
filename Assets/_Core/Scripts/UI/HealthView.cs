using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] Image _fillImage;

        private IDamagable _damagable;

        private Tweener _fillTweener;

        public void Setup(IDamagable damagable)
        {
            _damagable = damagable;

            _damagable.OnTakeDamage += UpdateHealthDisplay;
            UpdateHealthDisplay();
        }

        private void OnDisable()
        {
            if (_damagable != null)
                _damagable.OnTakeDamage -= UpdateHealthDisplay;
        }

        private void UpdateHealthDisplay()
        {
            HealthInfo healthInfo = _damagable.GetHealthInfo();

            float healthPercent = Mathf.InverseLerp(0, healthInfo.BaseHealth, healthInfo.CurrentHealth);

            _fillTweener.KillIfActiveAndPlaying();
            _fillTweener = _fillImage.DOFillAmount(healthPercent, 0.1f);
        }
    }
}