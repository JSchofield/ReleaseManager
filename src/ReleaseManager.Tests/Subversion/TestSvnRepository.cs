using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ReleaseManager.Subversion;

namespace ReleaseManager.Tests.Subversion
{
    [TestFixture]
    public class TestSvnRepository
    {
        [Test]
        public void CanGetLogEvents()
        {
            var stub = new StubSvnClient();
            var repo = new SvnRepository(stub);

            TimeSpan oneHour = TimeSpan.FromHours(1);
            var commitTime = new DateTime(2011,01,01);

            stub.AddEvent(commitTime += oneHour, "Me", "svn://repo/trunk/afolder/file1.cs", "file1.cs in a folder changed ");
            stub.AddEvent(commitTime += oneHour, "Me", "svn://repo/trunk//file2.cs", "file2.cs in trunk changed");
            stub.AddEvent(commitTime += oneHour, "Me", "svn://repo/trunk/afolder/file1.cs", "file1.cs in a folder changed");
            stub.AddEvent(commitTime += oneHour, "Me", "svn://repo/trunk/afolder/file3.cs", "TEST-23 file3.cs in a folder changed $ignore");

            IEnumerable<ILogItem> logItems = repo.GetLogItems("svn://repo/trunk/afolder", 2, null);

            Assert.AreEqual(2, logItems.Count());
        }
    }
}
