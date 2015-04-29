using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading
{
    class Program
    {
        public static void Main(string[] args)
        {
            var port = int.Parse(args[0]);
            AbstractServer server;
            var types = new Dictionary<string, Func<AbstractServer>>()
            {{"server", () => new Server(new ConcurrentReplacer(), "test2.txt", port)},
            {"threadserver", () => new ThreadServer(new ConcurrentReplacer(), "test2.txt", port)},
            {"taskserver", () => new TaskServer(new ConcurrentReplacer(), "test2.txt", port)}};
            server = types[args[1]]();
            server.Start();
            //CreateTestFile();
        }

        private static void CreateTestFile()
        {
            var str = new List<string>();
            var line = new StringBuilder();
            for (int i = 0; i < 150; i++)
            {
                line.Clear();
                for (int j = 0; j < 100; j++)
                    line.Append("word word word word");
                str.Add(line.ToString());
            }
            File.WriteAllLines(@"C:\Users\Администратор\Desktop\Nastya\Threading\Threading\Threading\test2.txt", str);

        }
    }
}
