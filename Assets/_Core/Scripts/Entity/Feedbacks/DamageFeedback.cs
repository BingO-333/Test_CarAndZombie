using UnityEngine;

namespace Game
{
    public abstract class DamageFeedback : MonoBehaviour
    {
        [SerializeField] GameObject _damagebleGO;

        protected IDamagable _damagable { get; private set; }

        private void OnValidate()
        {
            if (_damagebleGO != null && _damagebleGO.GetComponent<IDamagable>() == null)
                _damagebleGO = null;
        }

        protected virtual void Awake()
        {
            _damagable = _damagebleGO.GetComponent<IDamagable>();

            if (_damagable == null)
                return;

            _damagable.OnTakeDamage += PlayFeedback;
        }

        protected abstract void PlayFeedback();
    }
}