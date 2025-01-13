using UnityEngine;

namespace PG.Loading
{
    public class LoadingYield : CustomYieldInstruction
    {
        public override bool keepWaiting => KeepWaiting;
        public static bool KeepWaiting;
    }
}
