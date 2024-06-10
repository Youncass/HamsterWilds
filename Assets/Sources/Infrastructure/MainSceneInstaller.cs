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
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _playerPosition;
        [SerializeField] private InputActionAsset _playerControls;

        private readonly CompositeDisposable _disposables = new();

        public override void InstallBindings()
        {
            RegisterPlayerInput();
            CreatePlayer();


            void RegisterPlayerInput()
            {
                Container
                    .Bind<IPlayerInput>()
                    .FromInstance(new PlayerInput(_playerControls).AddTo(_disposables))
                    .AsSingle();
            }
        }
        
        private void CreatePlayer()
        {
            Container.InstantiatePrefab(
                _playerPrefab,
                _playerPosition.position,
                Quaternion.identity,
                parentTransform: null);
        }
    }
}
