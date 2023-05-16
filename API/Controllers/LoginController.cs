using API.DTOs;
using API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ItesrcneDocentesContext context;

        public LoginController(ItesrcneDocentesContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult Login(LoginDTO datos)
        {
            //No olvidar encriptrar password por segurad
            var dpto = context.Departamentos
                .SingleOrDefault(x => x.Clave == datos.Username && x.Contraseña == datos.Password);

            if (dpto == null || dpto.Eliminado == 1)//No existe
            {
                return Unauthorized("Clave de departamento ó contraseña incorrecto");
            }
            else
            {
                //1. Crear claims
                //2. Crear token
                //3. Regresar el token

                List<Claim> claims = new()
                {
                    new Claim("Id", dpto.Id.ToString()),
                    new Claim("Clave", dpto.Clave ?? ""),
                    new Claim(ClaimTypes.Name, dpto.Nombre),
                    new Claim(ClaimTypes.Email, dpto.Correo ?? ""),
                    new Claim(ClaimTypes.Role, "Departamento")//Impersonalizar
                };

                SecurityTokenDescriptor tokenDescriptor = new()
                {
                    Issuer = "docentes.itesrc.net",
                    Audience = "mauidocentes",
                    IssuedAt = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddHours(.5),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DocentesKeyMoviles83G")), SecurityAlgorithms.HmacSha256),
                    //Claims = (IDictionary<string, object>)claims
                    Subject = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme)
                };

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                var token = handler.CreateToken(tokenDescriptor);

                return Ok(handler.WriteToken(token));   
                //Cambios
            }
        }
    }
}
