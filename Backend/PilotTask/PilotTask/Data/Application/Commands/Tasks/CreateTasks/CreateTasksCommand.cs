namespace PilotTask.Data.Application.Commands.Tasks.CreateTasks
{
    public class CreateTasksCommand
    {
        public int ProfileId { get; set; }
        public string? TaskName { get; set; }
        public string? TaskDescription { get; set; }
        public DateTime? StartTime { get; set; }
        public int? Status { get; set; }
    }
}
