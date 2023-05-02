using System;
using Tutor.Tutorials;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Zenject;
using Zenject.Signals;

namespace Tutor.GameTutorials
{
    public class UITutorial : BaseLevelTutorial
    {
        public override bool IsTimeToRun() => true;

        public override async UniTask RunAsync(int curStep)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
            await base.RunAsync(curStep);
        }

        protected override UniTask? GetStateTask(int state, CancellationToken token)
        {
            return state switch
            {
                0 => DialogHello(token),
                _ => null,
            };
        }

        private async UniTask DialogHello(CancellationToken cancellationToken)
        {
            TutorialContainer.ShowDialogText("Привет!");
            await SignalBus.WaitForSignalAsync<CloseTextDialog>(cancellationToken);
            TutorialContainer.ShowDialogText("Это демонстрация работы тутора. Он работает при помощи Zenject, UniRX, UniTask!");
            await SignalBus.WaitForSignalAsync<CloseTextDialog>(cancellationToken);
            TutorialContainer.ShowDialogText("Спасибо за внимание!");
            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellationToken);
            TutorialContainer.ActivateButtonQuit();
        }
    }
}