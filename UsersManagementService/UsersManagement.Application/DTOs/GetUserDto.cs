namespace UsersManagement.Application.DTOs
{
    public class GetUserDto
    {
        public Guid UserId { get; set; }
        public string Login { get; set; } = default!;
    }
}
