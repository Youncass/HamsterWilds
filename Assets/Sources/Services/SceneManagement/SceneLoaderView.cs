using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Sources.Services.SceneManagement
{
    public class SceneLoaderView : MonoBehaviour
    {
        [SerializeField, Range(0.1f, 5f)] private float _fadeInDuration = 3f;
        [SerializeField, Range(0.1f, 5f)] private float _fadeOutDuration = 2f;

        [SerializeField] private CanvasGroup _screen;
        [SerializeField] private GameObject _loadingIcon;

        public void DisplayLoading(AsyncOperation sceneLoad, Action ended = null)
        {
            FadeIn(completed: () => StartCoroutine(Loading()));

            IEnumerator Loading()
            {
                _loadingIcon?.SetActive(true);

                while (sceneLoad.isDone == false)
                    yield return null;

                ended.Invoke();

                _loadingIcon?.SetActive(false);
                FadeOut();
            }
        }

        private void FadeIn(Action completed)
        {
            _screen.interactable = true;
            _screen.blocksRaycasts = true;

            _screen
                .DOFade(1f, _fadeInDuration)
                .SetEase(Ease.Linear)
                .OnComplete(new TweenCallback(completed));
        }

        private void FadeOut()
        {
            _screen
                .DOFade(0f, _fadeOutDuration)
                .SetEase(Ease.Linear);

            _screen.interactable = false;
            _screen.blocksRaycasts = false;
        }
    }
}
