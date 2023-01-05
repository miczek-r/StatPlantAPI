using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Hub> Hubs { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<Event> Events { get; set; }
    }
}
