using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Threading
{
    abstract public class AbstractServer
    {
        protected HttpListener listener;
        protected ConcurrentReplacer replacer;

        public AbstractServer(ConcurrentReplacer replacer, string filename, int port)
        {
            this.replacer = replacer;
            replacer.ReadFile(filename);
            listener = new HttpListener();
            listener.Prefixes.Add(string.Format(string.Format("http://127.0.0.1:{0}/method/", port)));
        }

        public void StartListen(System.Net.HttpListenerContext client)
        {
            string word = client.Request.QueryString["word"];
            string replace = client.Request.QueryString["replace"];
            IPEndPoint remoteEndPoint = client.Request.RemoteEndPoint;
            client.Request.InputStream.Close();
            var repr = replacer.ReplaceFirst(word, replace);
            var output = client.Response.OutputStream;
            byte[] bytes;
            if (repr != null)
            {
                bytes = Encoding.UTF8.GetBytes(string.Format("{0} {1}", repr.Item1, repr.Item2));
            }
            else
                bytes = Encoding.UTF8.GetBytes("null");
            output.Write(bytes, 0, bytes.Length);
            output.Close();
        }
        public abstract void Start();
    }
}
