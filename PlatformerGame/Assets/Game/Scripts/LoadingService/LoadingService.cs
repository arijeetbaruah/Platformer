using System.Collections;
using PG.Service;
using UnityEngine;

namespace PG.Loading
{
    public class LoadingService : IService
    {
        private LoadingSystem loadingSystem;

        public LoadingService(LoadingSystem loadingSystem)
        {
            this.loadingSystem = loadingSystem;
        }
        
        public void Update()
        {
            
        }

        public IEnumerator FadeIn() => loadingSystem.FadeIn();
        public IEnumerator FadeOut() => loadingSystem.FadeOut();

        public void SetProgress(float loaderPercentComplete) => loadingSystem.SetProgress(loaderPercentComplete);
    }
}
