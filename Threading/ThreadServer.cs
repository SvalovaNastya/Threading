using System.Threading;

namespace Threading
{
    public class ThreadServer : AbstractServer
    {

        public ThreadServer(ConcurrentReplacer replacer, string filename, int port)
            : base(replacer, filename, port) { }

        public override void Start()
        {
            listener.Start();
            while (true)
            {
                var client = listener.GetContext();
                new Thread(() => StartListen(client)).Start();
            }
        }

    }
}