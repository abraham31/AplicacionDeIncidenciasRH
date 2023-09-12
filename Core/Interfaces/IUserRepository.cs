using Core.Entities.Auth;
using Core.Entities.Auth.AuthDto;
using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        bool IsUsuarioUnico(string userName);

        Task<IdentityResult> ValidatePasswordAsync(UserApplication user, string password);

        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);

        Task<UserDto> Registrar(RegistroRequestDTO registroRequestDTO);
    }
}
