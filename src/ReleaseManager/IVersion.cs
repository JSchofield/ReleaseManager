using System.Collections.Generic;

namespace ReleaseManager
{
    public interface IVersion
    {
        IRelease Release { get; }
        IComponent Component { get; }

        long StartRevision { get; set; }
        long? EndRevision { get; set; }
        long? SelectedRevision { get; set; }
        
        IDictionary<long, IRevisionOverride> RevisionOverrides { get; }
        void SetOverride(long revisionNumber, bool canBeReleased, bool ignore, string setBy);
        void RemoveOverride(long revisionNumber);
    }
}

