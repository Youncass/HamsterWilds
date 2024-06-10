using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Infrastructure
{
    public class BootstrapLauncher : MonoBehaviour
    {
        private void Awake()
        {
            if (GameObject.FindWithTag(Tag.Bootstrap))
                return;

            SceneManager.LoadScene(Scene.Bootstrap);
        }
    }
}
