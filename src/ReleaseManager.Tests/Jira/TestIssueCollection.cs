namespace ReleaseManager.Tests.Core.Jira
{
    using System;
    using NUnit.Framework;
    using ReleaseManager.Jira;
    using System.Collections.Generic;

    public class IssueCollectionSpecs: BehaviorDrivenSpecificationBase
    {
        protected IssueCollection issueCollection;
        protected override void EstablishContext()
        {}

        protected override void When()
        {}
    }

    [TestFixture]
    [Category("IssueCollection")]
    public class When_IssueCollection_is_empty : IssueCollectionSpecs
    {
        protected override void When()
        {
            issueCollection = new IssueCollection(new List<IIssue>());
            Assert.IsEmpty(issueCollection);
        }

        [Test]
        public void CanBeReleased_is_false()
        {
            Assert.IsFalse(issueCollection.CanBeReleased);
        }
    }

    [TestFixture]
    [Category("IssueCollection")]
    public class When_IssueCollection_contains_one_unreleasable_issue : IssueCollectionSpecs
    {
        protected override void When()
        {
            issueCollection = 
                new IssueCollection(new List<IIssue>{
                    new Issue { CanBeReleased = false }});

            Assert.AreEqual(1, issueCollection.Count);
        }

        [Test]
        public void CanBeReleased_is_false()
        {
            Assert.IsFalse(issueCollection.CanBeReleased);
        }
    }

    [TestFixture]
    [Category("IssueCollection")]
    public class When_IssueCollection_contains_one_releasable_issue : IssueCollectionSpecs
    {
        protected override void When()
        {
            issueCollection =
                new IssueCollection(new List<IIssue>{
                    new Issue { CanBeReleased = true }});

            Assert.AreEqual(1, issueCollection.Count);
        }

        [Test]
        public void CanBeReleased_is_true()
        {
            Assert.IsTrue(issueCollection.CanBeReleased);
        }
    }

    [TestFixture]
    [Category("IssueCollection")]
    public class When_IssueCollection_contains_only_releasable_issues : IssueCollectionSpecs
    {
        protected override void When()
        {
            issueCollection =
                new IssueCollection(new List<IIssue>{
                    new Issue { CanBeReleased = true },
                    new Issue { CanBeReleased = true },
                    new Issue { CanBeReleased = true },
                    new Issue { CanBeReleased = true }});

            Assert.AreEqual(1, issueCollection.Count);
        }

        [Test]
        public void CanBeReleased_is_true()
        {
            Assert.IsTrue(issueCollection.CanBeReleased);
        }
    }

    [TestFixture]
    [Category("IssueCollection")]
    public class When_IssueCollection_contains_any_unreleasable_issues : IssueCollectionSpecs
    {
        protected override void When()
        {
            issueCollection =
                new IssueCollection(new List<IIssue>{
                    new Issue { CanBeReleased = true },
                    new Issue { CanBeReleased = true },
                    new Issue { CanBeReleased = false },
                    new Issue { CanBeReleased = true }});

        }

        [Test]
        public void CanBeReleased_is_false()
        {
            Assert.IsFalse(issueCollection.CanBeReleased);
        }
    }
}
