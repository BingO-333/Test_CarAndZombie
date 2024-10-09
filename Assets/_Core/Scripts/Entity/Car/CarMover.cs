using UnityEngine;
using Zenject;

namespace Game
{
    public class CarMover : MonoBehaviour
    {
        [SerializeField] float _speed = 1f;

        private LevelManager _levelManager;

        [Inject] void Construct(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        private void Update()
        {
            if (_levelManager.CurrentState != ELevelState.Running)
                return;

            MoveForward();
        }

        private void MoveForward()
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }
    }
}