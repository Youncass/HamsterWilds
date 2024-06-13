using UnityEngine;
using Zenject;
using Sources.StaticData;

namespace Sources.Infrastructure
{
    public class StaticDataInstaller : MonoInstaller
    {
        [SerializeField] private PlayerHovererData _hovererData;

        public override void InstallBindings()
        {
            RegisterData(_hovererData);
        }

        private void RegisterData<T>(T data)
            => Container.Bind<T>().FromInstance(data).AsSingle();
    }
}
