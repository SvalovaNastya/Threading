using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Threading
{
    public class ConcurrentReplacer : IReplacer
    {
        private List<string> lines;
        private readonly List<object> lockers;

        public ConcurrentReplacer()
        {
            lines = new List<string>();
            lockers = new List<object>();
        }

        public string[] GetText()
        {
            lock (lockers)
            {
                return lines.ToArray();
            }
        }

        public void ReadFile(string filename)
        {
            lock (lockers)
            {
                lines = File.ReadAllLines(filename).ToList();
                for (int i = 0; i < lines.Count; i++)
                    lockers.Add(new object());
            }
        }

        public Tuple<int, string> ReplaceFirst(string word, string replace)
        {
            var regex = new Regex(String.Format(@"{0}", word));
            int replacementInd = 0;
            int i = 0;
            try
            {
                for (; i < lines.Count; i++)
                {
                    lock (lockers[i])
                    {
                        var newLine = regex.Replace(lines[i], replace, 1);
                        if (newLine != lines[i])
                        {
                            lines[i] = newLine;
                            replacementInd = i;
                            break;
                        }
                    }
                }
                if (i != lines.Count)
                    return Tuple.Create(replacementInd, lines[i]);
            }
            catch
            {
                return null;
            }
            return null;
        }
    }
}
