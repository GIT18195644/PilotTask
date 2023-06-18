namespace PilotTask.Data.Application.Commands.Profiles.UpdateProfiles
{
    public class UpdateProfilesCommand
    {
        public int ProfileId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailId { get; set; }
    }
}
