namespace ReleaseManager.Web.Models
{
    using System;

    public class ReleaseStatusViewModel
    {
        public bool? CanBeReleased { get; set; }

        public bool? Ignore { get; set; }

        public bool IsBlocked { get; set; }

        public bool IsSet { get { return (CanBeReleased.HasValue && Ignore.HasValue); }}

        public string[] AllowableValues = new[] {"", "Not Ready", "Ready", "Ignore"};

        public string ReleaseStatus
        {
            get
            {
                if (!IsSet) return string.Empty;
                if (!CanBeReleased.Value && IsBlocked) return "Blocked";
                if (!CanBeReleased.Value) return "Not Ready";
                if (Ignore.Value) return "Ignored";
                return "Ready";
            }
            set
            {
                switch(value)
                {
                    case null:
                    case "":
                        this.CanBeReleased = null;
                        this.Ignore = null;
                        this.IsBlocked = false;
                        break;
                    case "Ignore":
                        this.CanBeReleased = true;
                        this.Ignore = true;
                        this.IsBlocked = false;
                        break;
                    case "Ready":
                        this.CanBeReleased = true;
                        this.Ignore = false;
                        this.IsBlocked = false;
                        break;
                    case "Not Ready":
                        this.CanBeReleased = false;
                        this.Ignore = false;
                        this.IsBlocked = false;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Invalid release status string");
                }

            }
        }
    }
}