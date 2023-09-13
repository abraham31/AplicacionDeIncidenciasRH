using AutoMapper;
using Core.Entities.Auth;
using Core.Entities.Auth.AuthDto;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private string secretKey;
        private readonly PasswordValidator<UserApplication> _passwordValidator;
        private readonly UserManager<UserApplication> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public UserRepository(ApplicationDbContext db, IConfiguration configuration, UserManager<UserApplication> userManager,
                                  IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            _passwordValidator = new PasswordValidator<UserApplication>();
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> ValidatePasswordAsync(UserApplication user, string password)
        {
            
            var validationResult = await _passwordValidator.ValidateAsync(_userManager, user, password);
            return validationResult;
        }

        public bool IsUsuarioUnico(string userName)
        {
            var user = _db.UserApplication.FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower());
            if (user == null)
            {
                return true;
            }   
            return false;
        }

        

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await _db.UserApplication.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());

            bool isValido = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (user == null || isValido == false)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }
            // Si Usuario Existe Generamos el JW Token
            var roles = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new()
            {
                Token = tokenHandler.WriteToken(token),
                User = _mapper.Map<UserDto>(user)
            };
            return loginResponseDTO;
        }

        public async Task<UserDto> Registrar(RegistroRequestDTO registroRequestDTO)
        {
            

            UserApplication user = new()
            {
                UserName = registroRequestDTO.UserName,
                Email = registroRequestDTO.UserName,
                NormalizedEmail = registroRequestDTO.UserName.ToUpper(),
                Names = registroRequestDTO.Names,
            };

            
                try
            {
                var resultado = await _userManager.CreateAsync(user, registroRequestDTO.Password);
                if (resultado.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole("admin"));
                        await _roleManager.CreateAsync(new IdentityRole("empleado"));
                    }


                    await _userManager.AddToRoleAsync(user, "admin");
                    var usuarioAp = _db.UserApplication.FirstOrDefault(u => u.UserName == registroRequestDTO.UserName);
                    return _mapper.Map<UserDto>(usuarioAp);
                }
            }
            catch (Exception)
            {
                
            }

            return new UserDto();

        }
    }
    
}
