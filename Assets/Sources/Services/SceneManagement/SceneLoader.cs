using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Services.SceneManagement
{
    public class SceneLoader : MonoBehaviour, ISceneLoader
    {
        [SerializeField] private SceneLoaderView _view;

        public void Load(string sceneName)
        {
            AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName);
            loading.allowSceneActivation = false;
            _view.DisplayLoading(loading, ended: () => loading.allowSceneActivation = true);
        }
    }
}
