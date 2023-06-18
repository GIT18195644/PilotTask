namespace PilotTask.Models
{
    public class TaskModel
    {
        public int? ProfileId { get; set; }
        public string? TaskName { get; set; }
        public string? TaskDescription { get; set; }
        public DateTime? StartTime { get; set; }
        public int? Status { get; set; }
    }
}
