using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Threading
{
    public class Replacer : IReplacer
    {
        private List<string> lines;

        public Replacer()
        {
            lines = new List<string>();
        }

        public string[] GetText()
        {
            return lines.ToArray();
        }

        public void ReadFile(string filename)
        {
            lines = File.ReadAllLines(filename).ToList();
        }

        public Tuple<int, string> ReplaceFirst(string word, string replace)
        {
            var regex = new Regex(String.Format(@"{0}", word));
            int replacementInd = 0;
            int i = 0;
            for (; i < lines.Count; i++)
            {
                var newLine = regex.Replace(lines[i], replace, 1);
                if (newLine != lines[i])
                {
                    lines[i] = newLine;
                    replacementInd = i;
                    break;
                }
            }
            if (i != lines.Count)
                return Tuple.Create(replacementInd, lines[i]);
            return null;
        }
    }
}
