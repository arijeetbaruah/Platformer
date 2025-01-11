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
        public IEnumerator LoadingProgression() => loadingSystem.LoadingProgression();
        public IEnumerator FadeOut() => loadingSystem.FadeOut();
    }
}
