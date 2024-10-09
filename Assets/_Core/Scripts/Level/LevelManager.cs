using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game
{
    public class LevelManager : MonoBehaviour
    {
        public event Action OnStateChanged;

        public ELevelState CurrentState { get; private set; } = ELevelState.Start;

        [SerializeField] Car _car;
        [SerializeField] FinishTrigger _finishTrigger;

        [Inject] void Construct(UIManager uiManager)
        {
            uiManager.OnStartButtonPressed += StartGame;
            uiManager.OnRestartButtonPressed += Restart;
        }

        private void Awake()
        {
            _car.OnDie += EndGame;
            _finishTrigger.OnCarFinish += EndGame;
        }

        private void StartGame()
        {
            ChangeState(ELevelState.Running);
        }

        private void EndGame()
        {
            ChangeState(ELevelState.Ended);
        }

        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void ChangeState(ELevelState newState)
        {
            if (CurrentState == newState)
                return;

            CurrentState = newState;
            OnStateChanged?.Invoke();
        }
    }
}