using System;

namespace ReleaseManager.NHibernate
{
    public class RevisionOverride : IRevisionOverride
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            var ci = obj as RevisionOverride;
            if (ci == null)
            {
                return false;
            }
            return ((ci.VersionId == this.VersionId) && (ci.Revision == this.Revision));
        }

        public override int GetHashCode()
        {
            return ((this.VersionId.GetHashCode() * 13) + (this.Revision.GetHashCode() * 0x25));
        }

        public virtual bool CanBeReleased { get; set; }
        public virtual bool Ignore { get; set; }
        public virtual bool Breaking { get; set; }

        public virtual string Note { get; set; }
        public virtual string SetBy { get; set; }
        public virtual DateTime SetTime { get; set; }
        public virtual long Revision{get;set;}
        public virtual int VersionId{get;set;}
    }
}

