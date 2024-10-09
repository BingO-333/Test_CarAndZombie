using SmartUI;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        public event Action OnStartButtonPressed;
        public event Action OnRestartButtonPressed;

        [SerializeField] Page_SUI _startPage;
        [SerializeField] Page_SUI _runningPage;
        [SerializeField] Page_SUI _endedPage;
        [Space]
        [SerializeField] Button _startButton;
        [SerializeField] Button _restartButton;

        private LevelManager _levelManager;

        [Inject] void Construct(LevelManager levelManaver)
        {
            _levelManager = levelManaver;
        }

        private void Awake()
        {
            _levelManager.OnStateChanged += UpdatePageByLevelState;
            UpdatePageByLevelState();

            _startButton.onClick.AddListener(() => OnStartButtonPressed?.Invoke());
            _restartButton.onClick.AddListener(() => OnRestartButtonPressed?.Invoke());
        }

        private void UpdatePageByLevelState()
        {
            _startPage.StartHiding();
            _runningPage.StartHiding();
            _endedPage.StartHiding();

            Page_SUI activePage = _levelManager.CurrentState switch
            {
                ELevelState.Start => _startPage,
                ELevelState.Running => _runningPage,
                ELevelState.Ended => _endedPage,
                _ => _endedPage
            };

            activePage.StartShowing();
        }
    }
}