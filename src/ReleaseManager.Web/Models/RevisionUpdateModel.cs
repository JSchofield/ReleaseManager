namespace ReleaseManager.Web.Models
{
    public class RevisionUpdateModel
    {
        public long Number { get; set; }
        public ReleaseStatusViewModel UserOverride { get; set; }
    }
}