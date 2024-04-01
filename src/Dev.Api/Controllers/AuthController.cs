using Dev.Business.Interfaces;
using Dev.Business.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
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
                        IOptions<JwtSettings> jwtSettings) : base(notificador)
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
         
            return CustomResponse(HttpStatusCode.OK, new AuthResponse { Token = GerarJwt() });
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

            return CustomResponse(HttpStatusCode.OK, new AuthResponse { Token = GerarJwt() });
        }

        [HttpPost("logout")] 
        public async Task<ActionResult> Logout()
        {
             await _signInManager.SignOutAsync();

            return CustomResponse();
        }

        private string GerarJwt()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Segredo);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
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
