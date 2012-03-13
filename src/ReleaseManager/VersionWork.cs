namespace ReleaseManager
{
    public class VersionWork : IVersionWork
    {
        private readonly IRevisionSetBuilder builder;
        private readonly IVersion version;
        private IRevisionCollection revisions;

        public IVersion VersionData()
        {
            return this.version;
        }

        public VersionWork(IVersion version, IRevisionSetBuilder builder)
        {
            this.version = version;
            this.builder = builder;
        }
        
        public IRevisionCollection Revisions
        {
            get { return this.revisions ?? (this.revisions = this.builder.CreateRevisionSet(this.version)); }
        }

        public IRelease Release
        {
            get { return this.version.Release; }
        }

        public IComponent Component
        {
            get { return this.version.Component; }
        }

        public long StartRevision
        {
            get
            {
                return this.version.StartRevision;
            }
            set
            {
                this.version.StartRevision = value;
            }
        }

        public long? EndRevision
        {
            get
            {
                return this.version.EndRevision;
            }
            set
            {
                this.version.EndRevision = value;
            }
        }

        public long? SelectedRevision
        {
            get
            {
                return this.version.SelectedRevision;
            }
            set
            {
                this.version.SelectedRevision = value;
            }
        }

        public System.Collections.Generic.IDictionary<long, IRevisionOverride> RevisionOverrides
        {
            get { return this.version.RevisionOverrides; }
        }

        public void SetOverride(long revisionNumber, bool canBeReleased, bool ignore, string setBy)
        {
            this.version.SetOverride(revisionNumber, canBeReleased, ignore, setBy);
        }

        public void RemoveOverride(long revisionNumber)
        {
            this.version.RemoveOverride(revisionNumber);
        }
    }
}