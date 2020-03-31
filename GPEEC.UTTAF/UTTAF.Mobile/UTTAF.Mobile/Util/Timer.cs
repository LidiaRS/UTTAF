using System;
using System.Threading;

using Xamarin.Forms;

namespace UTTAF.Mobile.Util
{
    internal class Timer
    {
        private CancellationTokenSource cancellation;

        internal TimeSpan Interval { get; set; }

        internal event Action Tick;

        internal Timer() => cancellation = new CancellationTokenSource();

        internal Timer(TimeSpan interval) : this() => Interval = interval;

        internal void Start()
        {
            CancellationTokenSource cts = cancellation;
            Device.StartTimer(Interval, () =>
            {
                if (cts.IsCancellationRequested)
                    return false;

                Tick.Invoke();

                return true;
            });
        }

        internal void Stop() => Interlocked.Exchange(ref cancellation, new CancellationTokenSource()).Cancel();
    }
}