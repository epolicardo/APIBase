using Data;
using Data.Entities;
using Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class Usuarios1Controller : ControllerBase
    {

        private readonly JwtBearerTokenSettings jwtBearerTokenSettings;
        private readonly UserManager<IdentityUser> userManager;
        private readonly DataContext context;
        private readonly IGenericRepository<Usuario> genericRepository;
        //private readonly IConfigurationHelper configHelper;

        public Usuarios1Controller(IOptions<JwtBearerTokenSettings> jwtTokenOptions, UserManager<IdentityUser> userManager, DataContext _context,
            IGenericRepository<Usuario> _genericRepository)
        {
            this.jwtBearerTokenSettings = jwtTokenOptions.Value;
            this.userManager = userManager;
            context = _context;
            genericRepository = _genericRepository;

        }

        [HttpGet]
        [Route("GetByMailAsync/{email}")]
        public Usuario GetByMailAsync(string email)
        {

            return context.Usuarios.Include(p => p.Persona).ThenInclude(d => d.Domicilio).FirstOrDefault(x => x.Email == email);

        }


        [Authorize]
        [HttpGet]
        [Route("GetByIdAsync")]
        public async Task<Usuario> GetByIdAsync(int Id)
        {


            return await genericRepository.GetByIdAsync(Id);

        }


        [Authorize]
        [HttpGet]
        [Route("GetList")]
        public IEnumerable<Usuario> GetList()
        {

            LogContext.PushProperty("Metodo", MethodBase.GetCurrentMethod());
            LogContext.PushProperty("Server", Environment.MachineName);
            IEnumerable<Usuario> Usuarios = null;
            try
            {

                Usuarios = context.Usuarios.ToList();
                Log.Information("Usuarios: {@Usuarios}", Usuarios);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Sucedio un error");
            }
            return Usuarios;
        }

        [Authorize]
        [HttpGet]
        [Route("GetListByGroup")]
        public IEnumerable<Usuario> GetListByGroup(string TituloGrupo)
        {
            LogContext.PushProperty("Metodo", MethodBase.GetCurrentMethod());
            LogContext.PushProperty("Server", Environment.MachineName);
            IEnumerable<Usuario> Usuarios = context.Usuarios.Include(x => x.Persona).Where(x => x.Persona.Apellido == TituloGrupo).ToList();
            Log.Information("Usuarios: {@Usuarios}", Usuarios);
            return Usuarios;

        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(Usuario usuario)
        {
            if (!ModelState.IsValid || usuario == null)
            {
                return new BadRequestObjectResult(new { Message = "User Registration Failed" });
            }

            usuario.Persona = new Persona();
                     
            usuario.Persona.FechaAlta = DateTime.Now;
       


            var result = await userManager.CreateAsync(usuario, usuario.Password);
            if (!result.Succeeded)
            {
                var dictionary = new ModelStateDictionary();
                foreach (IdentityError error in result.Errors)
                {
                    dictionary.AddModelError(error.Code, error.Description);
                }

                return new BadRequestObjectResult(new { Message = "User Registration Failed", Errors = dictionary });
            }



            await context.SaveChangesAsync();
            return Ok(new { Message = "User Registration Successful" });
        }


        [HttpPost]
        [Route("GetToken")]
        public async Task<IActionResult> GetToken(LoginCredentials credentials)
        {
            Log.Information("Credenciales ingresadas: {@Credenciales}", credentials);
            IdentityUser identityUser;

            if (!ModelState.IsValid
                || credentials == null
                || (identityUser = await ValidateUser(credentials)) == null)
            {
                return new BadRequestObjectResult(new { Message = "Login failed" });
            }

            var token = GenerateToken(identityUser);
            Log.Information("Message = Success, Email = {@Email}", identityUser.Email);
            return Ok(new { Token = token, Message = "Success", Email = identityUser.Email });
        }

        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout()
        {
            // Well, What do you want to do here ?
            // Wait for token to get expired OR 
            // Maintain token cache and invalidate the tokens after logout method is called
            return Ok(new { Token = "", Message = "Logged Out" });
        }

        private async Task<IdentityUser> ValidateUser(LoginCredentials credentials)
        {
            var identityUser = await userManager.FindByNameAsync(credentials.Username);
            if (identityUser != null)
            {
                var result = userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, credentials.Password);
                return result == PasswordVerificationResult.Failed ? null : identityUser;
            }

            return null;
        }


        private object GenerateToken(IdentityUser identityUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, identityUser.UserName.ToString()),
                    new Claim(ClaimTypes.Email, identityUser.Email)
                }),

                Expires = DateTime.UtcNow.AddSeconds(jwtBearerTokenSettings.ExpiryTimeInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = jwtBearerTokenSettings.Audience,
                Issuer = jwtBearerTokenSettings.Issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
