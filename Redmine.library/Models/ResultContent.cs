namespace Redmine.library.Models
{
    public abstract class ResultContent
    {
        public int TotalCount { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}