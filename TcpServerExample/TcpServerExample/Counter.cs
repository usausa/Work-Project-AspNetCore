namespace TcpServerExample
{
    using System.Threading;

    public class Counter
    {
        private int counter;

        public int IncrementAndGet()
        {
            return Interlocked.Add(ref counter, 1);
        }
    }
}
