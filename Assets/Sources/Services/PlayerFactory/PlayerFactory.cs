using Sources.Services.AssetManagement;
using Sources.GamePlay.Interaction;
using Sources.Services.Input;
using Sources.StaticData;
using UnityEngine;
using Zenject;
using UniRx;

namespace Sources.Services.PlayerFactory
{
    public class PlayerFactory : IPlayerFactory
    {
        private GameObject _player;

        private readonly IAssets _assets;
        private readonly IPlayerInput _input;
        private readonly IInstantiator _instantiator;
        private readonly PlayerHovererData _hovererData;

        private readonly CompositeDisposable _disposables = new();

        public PlayerFactory(
            IAssets assets,
            IPlayerInput input,
            IInstantiator instantiator,
            PlayerHovererData hovererData)
        {
            _assets = assets;
            _input = input;
            _instantiator = instantiator;
            _hovererData = hovererData;
        }

        public void CreatePlayer(Vector3 at)
        {
            _player = _instantiator.InstantiatePrefab(
                _assets.Load<GameObject>(AssetPath.Player),
                position: at,
                Quaternion.identity,
                parentTransform: null);

            var camera = _player.GetComponentInChildren<Camera>();

            var interactor = new PlayerInteractor(_player, _input).AddTo(_disposables);
            new PlayerHoverer(camera.transform, interactor, _hovererData).AddTo(_disposables);
        }

        public void DestroyPlayer()
        {
            _disposables.Dispose();
            Object.Destroy(_player);
        }
    }
}
