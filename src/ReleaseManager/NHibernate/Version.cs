using System;
using System.Collections.Generic;

namespace ReleaseManager.NHibernate
{
    public class Version: IVersion
    {
        public Version()
        {
            RevisionOverrides = new Dictionary<long, IRevisionOverride>();
        }

        public virtual Component Component { get; set; }

        public virtual long? EndRevision { get; set; }

        public virtual int Id { get; set; }

        public virtual Release Release { get; set; }

        public virtual IDictionary<long, IRevisionOverride> RevisionOverrides { get; protected internal set; }

        public virtual void SetOverride(
            long revisionNumber, 
            bool canBeReleased,
            bool ignore,
            string setBy)
        {
            RevisionOverrides[revisionNumber] = 
                new RevisionOverride {
                    CanBeReleased = canBeReleased,
                    Ignore = ignore,
                    SetBy = setBy,
                    SetTime = DateTime.Now};
        }

        public virtual void RemoveOverride(long revisionNumber)
        {
            RevisionOverrides.Remove(revisionNumber);
        }

        public virtual long? SelectedRevision { get; set; }

        public virtual long StartRevision { get; set; }

        IComponent IVersion.Component { get { return this.Component; } }

        IRelease IVersion.Release { get { return this.Release; } }
    }
}

