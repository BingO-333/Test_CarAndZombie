using Cinemachine;
using System;
using UnityEngine;
using Zenject;

namespace Game
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera _startVC;
        [SerializeField] CinemachineVirtualCamera _runningVC;

        private LevelManager _levelManager;

        [Inject] void Construct(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        private void Awake()
        {
            _levelManager.OnStateChanged += UpdateVicrtualCameraByState;
            UpdateVicrtualCameraByState();
        }

        private void UpdateVicrtualCameraByState()
        {
            _startVC.gameObject.SetActive(false);
            _runningVC.gameObject.SetActive(false);

            CinemachineVirtualCamera activeVC = _levelManager.CurrentState == ELevelState.Start 
                ? _startVC : _runningVC;

            activeVC.gameObject.SetActive(true);
        }
    }
}