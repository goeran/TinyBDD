using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using System.Threading;
using TinyBDD.Specification.NUnit;

namespace TinyBDDTests.Learning
{
    [TestFixture]
    public class FileSystemWatcherTests
    {
        FileSystemWatcher fsWatcher;
        string directoryToMonitor = Environment.CurrentDirectory;

        [SetUp]
        public void Setup()
        {
            fsWatcher = new FileSystemWatcher(directoryToMonitor);
            fsWatcher.EnableRaisingEvents = true;

            foreach (var file in Directory.GetFiles(directoryToMonitor))
                if (file.EndsWith(".test"))
                    File.Delete(file);
        }

        [Test]
        public void Should_trigger_notify_when_file_created()
        {
            bool notified = false;

            fsWatcher.Created += (o, e) =>
            {
                notified = true;
            };

            File.Create(
                Path.Combine(directoryToMonitor, "goeran.test"));

            Thread.Sleep(50);

            notified.ShouldBeTrue();

        }
    }
}
