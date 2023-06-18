using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PilotTask.Data.Entities
{
    public class Tasks
    {
        [Key]
        public int? Id { get; set; }

        public int? ProfileId { get; set; }

        [Column(TypeName = "VARCHAR(1000)")]
        public string? TaskName { get; set; }

        [Column(TypeName = "VARCHAR(1000)")]
        public string? TaskDescription { get; set; }

        public DateTime? StartTime { get; set; }

        public int? Status { get; set; }
    }
}
