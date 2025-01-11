using UnityEngine;

namespace PG.Framework
{
    public enum PlatformType
    {
        Grass,
        Gravel
    }
    
    public class Platform : MonoBehaviour
    {
        [field:SerializeField] public PlatformType platformType { get; private set; } = PlatformType.Grass;
    }
}
