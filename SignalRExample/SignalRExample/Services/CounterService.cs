namespace SignalRExample.Services
{
    using System.Threading.Tasks;

    public interface ICounterNotifier
    {
        Task Update();
    }

    public class CounterService
    {
        private readonly object sync = new object();

        private int counter;

        private ICounterNotifier CounterNotifier { get; }

        public CounterService(ICounterNotifier counterNotifier)
        {
            CounterNotifier = counterNotifier;
        }

        public int Query()
        {
            lock (sync)
            {
                return counter;
            }
        }

        public Task Increment()
        {
            lock (sync)
            {
                counter++;
            }

            return CounterNotifier.Update();
        }
    }
}
