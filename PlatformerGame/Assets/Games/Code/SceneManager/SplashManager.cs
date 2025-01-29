using System.Collections;
using BP.Config;
using BP.Input;
using BP.Service;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BP.SceneManager
{
    public class SplashManager : MonoBehaviour
    {
        [SerializeField]
        private ConfigRegistryAssetReference configRegistry;
        
        private IEnumerator Start()
        {
            ServiceManager.Add(new InputService());
            
            var handle = configRegistry.LoadAssetAsync<ConfigRegistry>();
            yield return handle;
            
            Debug.Log("Loaded Config Registry");
            ServiceManager.Add(new ConfigService(handle.Result));
        }
    }

    [System.Serializable]
    public class ConfigRegistryAssetReference : AssetReferenceT<ConfigRegistry>
    {
        public ConfigRegistryAssetReference(string guid) : base(guid)
        {
        }

        public override bool ValidateAsset(Object obj)
        {
            return obj is ConfigRegistry;
        }

        public override bool ValidateAsset(string mainAssetPath)
        {
#if UNITY_EDITOR
            var cr = UnityEditor.AssetDatabase.LoadAssetAtPath<ConfigRegistry>(mainAssetPath);
            
            return cr != null;
#else
            return base.ValidateAsset(mainAssetPath);
#endif
        }
    }
}
