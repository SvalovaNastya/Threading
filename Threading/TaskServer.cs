using System.Threading.Tasks;

namespace Threading
{
    public class TaskServer : AbstractServer
    {
        public TaskServer(ConcurrentReplacer replacer, string filename, int port)
            : base(replacer, filename, port) { }

        public override void Start()
        {
            listener.Start();
            while (true)
            {
                var client = listener.GetContext();
                Task.Factory.StartNew(() => StartListen(client));
            }
        }
    }
}