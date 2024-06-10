using UnityEngine;
using Zenject;

namespace Sources.Infrastructure
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _playerPosition;

        public override void InstallBindings()
        {
            CreatePlayer();
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
