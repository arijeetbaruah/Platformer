using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BP.Config
{
    [CreateAssetMenu(fileName = "ConfigRegistry", menuName = "Config Registry")]
    public class ConfigRegistry : SerializedScriptableObject
    {
        public List<BaseConfig> configs = new();
    }
}
