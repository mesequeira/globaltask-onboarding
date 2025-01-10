namespace Users.Worker.Domain.Users.Events
{
    public class UserCreatedEvent
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
