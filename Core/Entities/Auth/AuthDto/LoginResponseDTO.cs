namespace Core.Entities.Auth.AuthDto
{
    public class LoginResponseDTO
    {
        public UserDto User { get; set; }
        public string Token { get; set; }

        // public string Rol { get; set; }
    }
}
