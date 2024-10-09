using UnityEngine;
using Zenject;

namespace Game
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] LevelManager _levelManager;
        [SerializeField] UIManager _uiManager;

        public override void InstallBindings()
        {
            Container.Bind<LevelManager>().FromInstance(_levelManager).AsSingle();
            Container.Bind<UIManager>().FromInstance(_uiManager).AsSingle();
        }
    }
}