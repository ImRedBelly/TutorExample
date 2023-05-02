using System;
using System.Threading;
using JetBrains.Annotations;
using Cysharp.Threading.Tasks;

namespace Tutor.Tutorials
{
    [UsedImplicitly]
    public class BaseLevelTutorial : TutorialStateMachine
    {
        public TutorialSceneContainer TutorialContainer { get; private set; }

        public void InitializeTutorial(TutorialSceneContainer tutorialContainer) =>
            TutorialContainer = tutorialContainer;

        public override bool IsTimeToRun() => true;

        public override async UniTask RunAsync(int curStep)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
            await base.RunAsync(curStep);
        }

        protected override UniTask? GetStateTask(int state, CancellationToken token)
        {
            return null;
        }
    }
}