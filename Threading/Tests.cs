using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using System;

namespace Threading
{
    [TestFixture]
    public class ReplacerTests
    {
        private IReplacer replacer;

        private void RunReplace()
        {
            for (int i = 0; i < 15; i++)
            {
                var tup = replacer.ReplaceFirst("aa", "bb");
                Assert.IsNotNull(tup);
            }
        }

        [Test]
        public void Replacement()
        {
            replacer = new Replacer();
            replacer.ReadFile("test1.txt");
            Task t1 = Task.Run(() => RunReplace());
            Task t2 = Task.Run(() => RunReplace());
            Task.WaitAll(new[] { t1, t2 });
            var str = replacer.GetText()[0].Split(' ').ToArray();
            Assert.AreEqual("bb", str[str.Length - 1]);
        }

        [Test]
        public void ReplacementIsCorrect()
        {
            replacer = new Replacer();
            replacer.ReadFile("test1.txt");
            RunReplace();
            var str = replacer.GetText()[0].Split(' ').ToArray();
            Assert.AreEqual("bb", str[14]);
        }

        [Test]
        public void ConcurrentReplacementIsCorrect()
        {
            replacer = new ConcurrentReplacer();
            replacer.ReadFile("test1.txt");
            RunReplace();
            var str = replacer.GetText()[0].Split(' ').ToArray();
            Assert.AreEqual("bb", str[14]);
        }

        [Test]
        public void ConcurrentReplacement()
        {
            replacer = new ConcurrentReplacer();
            replacer.ReadFile("test1.txt");
            Task t1 = Task.Run(() => RunReplace());
            Task t2 = Task.Run(() => RunReplace());
            Task.WaitAll(new[] { t1, t2 });
            var str = replacer.GetText()[0].Split(' ').ToArray();
            Assert.AreEqual("bb", str[str.Length - 1]);
        }

        [Test]
        public void ConcurrentReplacementOneThread()
        {
            replacer = new ConcurrentReplacer();
            replacer.ReadFile("test2.txt");
            for (int i = 0; i < 300; i++)
            {
                replacer.ReplaceFirst("Слово", "a");
            }
        }

        [Test]
        public void ConcurrentReplacementManyThreads()
        {
            replacer = new ConcurrentReplacer();
            replacer.ReadFile("test2.txt");
            for (int i = 0; i < 300; i++)
            {
                new Thread(() => replacer.ReplaceFirst("Слово", "a")).Start();
            }
        }

        [Test]
        public void ConcurrentReplacementManyTasks()
        {
            replacer = new ConcurrentReplacer();
            replacer.ReadFile("test2.txt");
            for (int i = 0; i < 300; i++)
            {
                Task.Factory.StartNew(() => replacer.ReplaceFirst("Слово", "a"));
            }

        }
    }
}