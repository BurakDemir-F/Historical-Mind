using UnityEngine;

namespace Algorithms
{
    public interface IRunOnFrames
    {
        public Coroutine PerformCor(int waitStep, YieldInstruction wait);
    }
}
