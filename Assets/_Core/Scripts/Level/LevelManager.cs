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
            _car.OnDie += Lose;
            _finishTrigger.OnCarFinish += Win;
        }

        private void StartGame()
        {
            ChangeState(ELevelState.Running);
        }

        private void Lose()
        {
            if (CurrentState == ELevelState.Win || CurrentState == ELevelState.Lose)
                return;

            ChangeState(ELevelState.Lose);
        }

        private void Win()
        {
            if (CurrentState == ELevelState.Win || CurrentState == ELevelState.Lose)
                return;

            ChangeState(ELevelState.Win);
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