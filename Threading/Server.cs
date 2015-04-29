
namespace Threading
{
    public class Server : AbstractServer
    {

        public Server(ConcurrentReplacer replacer, string filename, int port)
            : base(replacer, filename, port) { }

        public override void Start()
        {
            listener.Start();
            while (true)
            {
                var client = listener.GetContext();
                StartListen(client);
            }
        }
        
    }
}
