namespace Minenetred.web.Models
{
    public class TimeEntryFormDto
    {
        public int IssueId { get; set; }
        public string SpentOn { get; set; }
        public float Hours { get; set; }
        public int ActivityId { get; set; }
        public string Comments { get; set; }
    }
}