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
        [SerializeField] Page_SUI _losePage;
        [SerializeField] Page_SUI _winPage;
        [Space]
        [SerializeField] Transform _viewsContainer;
        [SerializeField] FollowerHealthView _followerHealthViewPrefab;

        private MonoPool<FollowerHealthView> _followerHealthViewsPool;

        private LevelManager _levelManager;

        [Inject] void Construct(LevelManager levelManaver)
        {
            _levelManager = levelManaver;
        }

        private void Awake()
        {
            _followerHealthViewsPool = new MonoPool<FollowerHealthView>(SpawnFollowerHealthView);

            _levelManager.OnStateChanged += UpdatePageByLevelState;
            UpdatePageByLevelState();
        }

        public void StartClick() => OnStartButtonPressed?.Invoke();
        public void RestartClick() => OnRestartButtonPressed?.Invoke();

        public FollowerHealthView GetFollowerHealthView() => _followerHealthViewsPool.Get();

        private void UpdatePageByLevelState()
        {
            _startPage.StartHiding();
            _runningPage.StartHiding();
            _winPage.StartHiding();
            _losePage.StartHiding();

            Page_SUI activePage = _levelManager.CurrentState switch
            {
                ELevelState.Start => _startPage,
                ELevelState.Running => _runningPage,
                ELevelState.Win => _winPage,
                ELevelState.Lose => _losePage,
                _ => _losePage
            };

            activePage.StartShowing();
        }

        private FollowerHealthView SpawnFollowerHealthView()
        {
            FollowerHealthView spawnedFHV = Instantiate(_followerHealthViewPrefab, _viewsContainer);
            spawnedFHV.gameObject.SetActive(false);

            return spawnedFHV;
        }
    }
}