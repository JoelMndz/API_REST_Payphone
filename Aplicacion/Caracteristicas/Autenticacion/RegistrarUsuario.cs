using Aplicacion.Dominio.Comunes;
using Aplicacion.Dominio.Usuario;
using Aplicacion.Dominio.Usuario.Errores;
using Aplicacion.DTOs;
using Aplicacion.Helper.Comunes;
using Aplicacion.Infraestructura.Persistencia;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Throw;

namespace Aplicacion.Caracteristicas.Autenticacion
{
    public class RegistrarUsuario
    {
        public class DatosRegistrarUsuario
        {
            public string? UserName { get; set; }
            public string? Clave { get; set; }
        }
        public record Comando(DatosRegistrarUsuario Datos):IRequest<UsuarioDTO>;
        public class ValidadorComando:AbstractValidator<Comando>
        {
            public ValidadorComando()
            {
                RuleFor(x => x.Datos.UserName)
                    .NotNull()
                    .MinimumLength(4)
                    .MaximumLength(25)
                    .Must(x => x == null ? true : Regex.IsMatch(x, @"^[A-Za-z\s]+$"));

                RuleFor(x => x.Datos.Clave)
                    .NotNull()
                    .MinimumLength(8)
                    .MaximumLength(50)
                    .Must(x => x == null ? true : Regex.IsMatch(x, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$"));
            }
        }

        public class Handler : IRequestHandler<Comando, UsuarioDTO>
        {
            private readonly Contexto contexto;
            private readonly IMapper mapper;

            public Handler(Contexto contexto, IMapper mapper)
            {
                this.contexto = contexto;
                this.mapper = mapper;
            }
            public async Task<UsuarioDTO> Handle(Comando request, CancellationToken cancellationToken)
            {
                var usuario = Usuario.Crear(request.Datos.UserName!, request.Datos.Clave!);
                await ValidarUsernameExistente(request.Datos.UserName!);

                usuario.Clave = Criptografia.Encriptar(request.Datos.Clave!);
                contexto.Usuario.Add(usuario);

                (await contexto.SaveChangesAsync())
                    .Throw(() => new Errores.NoSePuedieronGuardarLosCambios()).IfTrue(x => x == 0);
            
                return mapper.Map<UsuarioDTO>(usuario);
            }

            private async Task ValidarUsernameExistente(string userName)
            {
                var usuarioExistente = await contexto.Usuario.FirstOrDefaultAsync(x => x.UserName == userName.Trim().ToUpper() && !x.Eliminado);
                if (usuarioExistente != null) throw new ErroresUsuario.ElUserNameYaExiste(userName);
            }
        }
    }
}
