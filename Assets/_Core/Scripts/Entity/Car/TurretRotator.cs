using UnityEngine;
using Zenject;

namespace Game
{
    public class TurretRotator : MonoBehaviour
    {
        [SerializeField] Transform _pivot;
        [SerializeField] float _rotateSpeed = 1f;
        [SerializeField] float _maxAngle = 75f;

        private InputManager _inputManager;
        private LevelManager _levelManager;

        private float _angleY;

        [Inject] void Construct(InputManager inputManager, LevelManager levelManager)
        {
            _inputManager = inputManager;
            _levelManager = levelManager;
        }

        private void Update()
        {
            if (_levelManager.CurrentState != ELevelState.Running)
                return;

            RotatePivot();
        }

        private void RotatePivot()
        {
            float horizontalDelta = _inputManager.SwipeDelta.x * 0.1f;

            _angleY += horizontalDelta; 
            _angleY = Mathf.Clamp(_angleY, -_maxAngle, _maxAngle);

            _pivot.localRotation = Quaternion.Euler(0, _angleY, 0);
        }
    }
}