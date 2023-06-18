namespace PilotTask.Data.Application.Commands.Profiles.CreateProfiles
{
    public class CreateProfilesCommand
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailId { get; set; }
    }
}
