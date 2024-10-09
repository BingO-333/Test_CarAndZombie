using UnityEngine;
using Zenject;

namespace Game
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] InputManager _inputManager;

        public override void InstallBindings()
        {
            Container.Bind<InputManager>().FromInstance(_inputManager).AsSingle();
        }
    }
}