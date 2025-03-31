namespace ScreenOps.AuthenticationService.Dtos
{
    public class UserSessionDto
    {
        public required UserDto User { get; set; }
        public required string Token { get; set; }
        public required DateTime ExpiresAt { get; set; }
        public required DateTime CreatedAt { get; set; }
    }
}
