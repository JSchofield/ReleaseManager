namespace ReleaseManager.Subversion
{
    public class SvnInfoEventArgsWrapper
    {
        public SvnInfoEventArgsWrapper(long lastRevision)
        {
            LastChangeRevision = lastRevision;
        }

        public long LastChangeRevision { get; set; }
    }
}
