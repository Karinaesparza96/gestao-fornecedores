using Dev.Business.Interfaces;
using Dev.Business.Models;
using Dev.Business.Models.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Dev.Api.Controllers
{
    [Route("api/conta")]
    public class AuthController : MainController
    {   
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        public AuthController(INotificador notificador, 
                              SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager, 
                              IOptions<JwtSettings> jwtSettings,
                              IAppIdentityUser user) : base(notificador, user)
        {   
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar(RegisterUserViewModel registerUser)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                Email = registerUser.Email,
                UserName = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (!result.Succeeded)
            {
                NotificarErro("Falha ao registrar o usuário");
                return CustomResponse();
            }
         
            return CustomResponse(HttpStatusCode.OK, new AuthResponse { Token = await GerarJwt(user.Email) });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, isPersistent: false, lockoutOnFailure: true);

            if(!result.Succeeded)
            {
                NotificarErro("Usuário ou senha incorretos.");
                return CustomResponse();
            }

            return CustomResponse(HttpStatusCode.OK, new AuthResponse { Token = await GerarJwt(loginUser.Email) });
        }

        [HttpPost("logout")] 
        public async Task<ActionResult> Logout()
        {
             await _signInManager.SignOutAsync();

            return CustomResponse();
        }

        private async Task<string> GerarJwt(string email)
        {   
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Segredo);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {   
                Subject = new ClaimsIdentity(claims),
                Issuer = _jwtSettings.Emissor,
                Audience = _jwtSettings.Audiencia,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return encodedToken;
        }
    }
}
