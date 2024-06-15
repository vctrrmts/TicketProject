namespace Auth.Application.Dtos
{
    public class JwtTokenDto
    {
        public string JwtToken { get; set; } = default!;
        public Guid RefreshToken { get; set; } = default!;
        public DateTime Expires { get; set; }
    }
}
