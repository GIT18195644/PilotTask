using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PilotTask.Data.Entities
{
    public class Profiles
    {
        [Key]
        public int ProfileId { get; set; }

        [Column(TypeName = "VARCHAR(1000)")]
        public string FirstName { get; set; }
        
        [Column(TypeName = "VARCHAR(1000)")]
        public string LastName { get; set; }
        
        public DateTime DateOfBirth { get; set; }

        [Column(TypeName = "VARCHAR(1000)")]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "VARCHAR(1000)")]
        public string EmailId { get; set; }
    }
}
