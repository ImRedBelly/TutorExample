using System;
using Zenject;
using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject.Signals;

namespace Tutor.Tutorials
{
    public abstract class TutorialStateMachine : TutorialBase
    {
        [Inject] protected readonly SignalBus SignalBus;
        private CancellationTokenSource _token;


        public override async UniTask RunAsync(int curStep)
        {
            _token = new CancellationTokenSource();
            var curStateTask = GetStateTask(curStep, _token.Token);
            while (curStateTask.HasValue)
            {
                try
                {
                    await curStateTask.Value;
                }
                catch (OperationCanceledException e)
                {
                    return;
                }


                curStep += 1;
                curStateTask = GetStateTask(curStep, _token.Token);

                await UniTask.Yield();
            }
        }

        public void Construct() =>
            SignalBus.Subscribe<CancelTutorTokenSignal>(CancelTask);

        public void Deconstruct() =>
            SignalBus.TryUnsubscribe<CancelTutorTokenSignal>(CancelTask);

        private void CancelTask() => _token?.Cancel();
        
        protected abstract UniTask? GetStateTask(int state, CancellationToken token);
    }
}