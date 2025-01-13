using System;
using System.Collections;
using PG.Service;
using UnityEngine;
using UnityEngine.UI;

namespace PG.Loading
{
    public class LoadingSystem : MonoBehaviour
    {
        [SerializeField] private Transform panel;
        [SerializeField] private Slider progressBar;
        [SerializeField] private float fadeInTime;
        [SerializeField] private float fadeOutTime;
        [SerializeField] private CanvasGroup canvasGroup;

        private void Start()
        {
            ServiceManager.Add(new LoadingService(this));
            progressBar.value = 0;
            panel.gameObject.SetActive(false);
        }

        public IEnumerator LoadingProgression()
        {
            float timer = 0;
            while (progressBar.value < 0.9f)
            {
                progressBar.value = timer;
                timer += Time.deltaTime * 2;
                yield return null;
            }
        }

        public IEnumerator FadeIn()
        {
            canvasGroup.alpha = 0;
            progressBar.value = 0;
            panel.gameObject.SetActive(true);

            float timer = 0;
            
            do
            {
                timer += Time.deltaTime;
                canvasGroup.alpha = timer / fadeInTime;
                yield return new WaitForEndOfFrame();
            }while(timer <= fadeInTime);
        }

        public IEnumerator FadeOut()
        {
            canvasGroup.alpha = 1;
            
            float timer = 0;
            
            do
            {
                timer += Time.deltaTime;
                canvasGroup.alpha = 1 - timer / fadeInTime;
                yield return new WaitForEndOfFrame();
            }while(timer <= fadeOutTime);
        }

        public void SetProgress(float loaderPercentComplete)
        {
            Debug.Log(loaderPercentComplete);
            progressBar.value = loaderPercentComplete;
        }
    }
}
