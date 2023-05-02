using UniRx;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Zenject
{
    public static class ZenjectSignalsExtensions
    {
        public static async UniTask<TSignal> WaitForSignalAsync<TSignal>(this SignalBus signalBus,
            CancellationToken cancellationToken = default)
            => await signalBus.GetStream<TSignal>().ToUniTask(true, cancellationToken);

        public static async UniTask<TSignal> WaitForSignalAsync<TSignal>(this SignalBus signalBus,
            System.Func<TSignal, bool> where, CancellationToken cancellationToken = default)
            => await signalBus.GetStream<TSignal>().Where(where).ToUniTask(true, cancellationToken);

        public static async UniTask WaitForAnySignalAsync<TSignal1, TSignal2>(this SignalBus signalBus,
            CancellationToken cancellationToken = default)
        {
            var task1 = signalBus.WaitForSignalAsync<TSignal1>(cancellationToken);
            var task2 = signalBus.WaitForSignalAsync<TSignal2>(cancellationToken);

            await UniTask.WhenAny(task1, task2);
        }

        public static SignalBus OnlyOnceOn<TSignal>(this SignalBus signalBus,
            System.Action<TSignal> action,
            System.Func<TSignal, bool> where = null)
        {
            if (where != null)
            {
                signalBus.GetStream<TSignal>().Where(where).First().Subscribe(action);
            }
            else
            {
                signalBus.GetStream<TSignal>().First().Subscribe(action);
            }

            return signalBus;
        }

        public static SignalBus OnlyOnceOn<TSignal>(this SignalBus signalBus, System.Action action,
            System.Func<TSignal, bool> where = null)
        {
            if (where != null)
            {
                signalBus.GetStream<TSignal>().Where(where).First().Subscribe(_ => action());
            }
            else
            {
                signalBus.GetStream<TSignal>().First().Subscribe(_ => action());
            }

            return signalBus;
        }
    }
}