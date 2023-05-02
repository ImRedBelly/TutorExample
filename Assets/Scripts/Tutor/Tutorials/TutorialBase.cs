using Cysharp.Threading.Tasks;

namespace Tutor.Tutorials
{
    public abstract class TutorialBase
    {
        public abstract bool IsTimeToRun();
        public abstract UniTask RunAsync(int curStep);
    }
}