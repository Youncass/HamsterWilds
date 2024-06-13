using Sources.Services.AssetManagement;
using Sources.Services.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoader _sceneLoader;

        public override void InstallBindings()
        {
            RegisterAssets();
            RegisterSceneLoader();

            SceneManager.LoadScene(Scene.Main);


            void RegisterAssets()
                => Container.Bind<IAssets>().To<Assets>().AsSingle();

            void RegisterSceneLoader()
                => Container.Bind<ISceneLoader>().FromInstance(_sceneLoader).AsSingle();
        }
    }
}
