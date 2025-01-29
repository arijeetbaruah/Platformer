using System.Collections.Generic;
using System.Linq;
using BP.Service;

namespace BP.Config
{
    public class ConfigService : IService
    {
        private Dictionary<System.Type, BaseConfig> configs;

        public ConfigService(ConfigRegistry configRegistry)
        {
            configs = configRegistry.configs.ToDictionary(registry => registry.GetType());
        }

        public TConfig Get<TConfig>() where TConfig : BaseConfig
        {
            return configs[typeof(TConfig)] as TConfig;
        }
        
        public void Update()
        {
            
        }

        public void OnDestroy()
        {
            
        }
    }
}
