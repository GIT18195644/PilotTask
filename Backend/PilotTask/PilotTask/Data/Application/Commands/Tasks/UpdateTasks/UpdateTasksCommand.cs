namespace PilotTask.Data.Application.Commands.Tasks.UpdateTasks
{
    public class UpdateTasksCommand
    {
        public int TaskId { get; set; }
        public int ProfileId { get; set; }
        public string? TaskName { get; set; }
        public string? TaskDescription { get; set; }
        public DateTime? StartTime { get; set; }
        public int? Status { get; set; }
    }
}
