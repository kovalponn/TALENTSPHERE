namespace TALENTSPHERE.Models
{
    public class VideoCall
    {
        public long Id { get; set; }
        public VideoCallStatus Status { get; set; }
        public long[]? Participants { get; set; }
        public DateTime CreateTime { get; set; }
        public int DurationHour { get; set; }
    }

    public enum VideoCallStatus
    {
        Plane, GoingOn
    }
}
