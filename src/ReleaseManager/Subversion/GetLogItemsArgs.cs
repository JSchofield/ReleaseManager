using System;

namespace ReleaseManager.Subversion
{
    public struct GetLogItemsArgs
    {
        public string Target { get; set; }
        public long StartRevision { get; set; }
        public long? EndRevision { get; set; }

        static public bool operator  ==(GetLogItemsArgs first, GetLogItemsArgs second)
        {
            return first.Equals(second);
        }

        static public bool operator !=(GetLogItemsArgs first, GetLogItemsArgs second)
        {
            return !first.Equals(second);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (GetLogItemsArgs) obj;

            return
                Target == other.Target
                && StartRevision == other.StartRevision
                && EndRevision == other.EndRevision;
        }

        public override int GetHashCode()
        {
            return
                Target.GetHashCode() * 13
                + StartRevision.GetHashCode() * 27
                + EndRevision.GetHashCode() * 31;
        }
    }
}