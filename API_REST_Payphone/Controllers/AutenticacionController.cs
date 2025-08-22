using API_REST_Payphone.Controllers.Comunes;
using Aplicacion.Caracteristicas.Autenticacion;
using Aplicacion.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_REST_Payphone.Controllers
{
    public class AutenticacionController:ApiBaseController
    {
        [HttpPost("registro")]
        public async Task<ActionResult> RegustrarUsuario(RegistrarUsuario.DatosRegistrarUsuario request)
        {
            var usuario = await Mediador.Send(new RegistrarUsuario.Comando(request));
            return Ok(usuario);
        }

        [HttpPost("inciar-sesion")]
        public async Task<ActionResult> IniciarSesion(IniciarSesion.DatosIniciarSesion request)
        {
            var usuario = await Mediador.Send(new IniciarSesion.Comando(request));
            return Ok(new
            {
                usuario,
                jwt = GenerarJWT(usuario)
            });
        }

        [Authorize]
        [HttpGet("endpoint-protegido")]
        public async Task<ActionResult> EdnpointProtegido()
        {
            return Ok("Hola, soy el endpoint protegido :)");
        }

        private string GenerarJWT(UsuarioDTO usuario)
        {
            var claims = new[]
            {
                    new Claim("id", usuario.Id.ToString()),
                    new Claim("username", usuario.UserName),
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRETO_JWT")!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(7);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
