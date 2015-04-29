using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Threading
{
    interface IReplacer
    {
        void ReadFile(string filename);
        Tuple<int, string> ReplaceFirst(string word, string replace);
        string[] GetText();
    }
}
