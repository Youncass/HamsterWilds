using Sources.Services.PlayerFactory;
using Sources.Services.Input;
using UnityEngine.InputSystem;
using UnityEngine;
using Zenject;
using UniRx;

using PlayerInput = Sources.Services.Input.PlayerInput;

namespace Sources.Infrastructure
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private Transform _playerPosition;
        [SerializeField] private InputActionAsset _playerControls;

        private PlayerFactory _playerFactory;

        private readonly CompositeDisposable _disposables = new();

        public override void InstallBindings()
        {
            RegisterPlayerInput();
            RegisterPlayerFactory();
            CreatePlayer();


            void RegisterPlayerInput()
            {
                Container
                    .Bind<IPlayerInput>()
                    .FromInstance(new PlayerInput(_playerControls).AddTo(_disposables))
                    .AsSingle();
            }

            void RegisterPlayerFactory()
            {
                _playerFactory = Container.New<PlayerFactory>();
                Container.Bind<IPlayerFactory>().FromInstance(_playerFactory).AsSingle();
            }
        }

        private void CreatePlayer()
        {
            _playerFactory.CreatePlayer(at: _playerPosition.position);
        }

        private void OnDestroy()
        {
            _playerFactory.DestroyPlayer();
        }
    }
}
