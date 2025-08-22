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
    public class IniciarSesion
    {
        public class DatosIniciarSesion
        {
            public string? UserName { get; set; }
            public string? Clave  { get; set; }
        }
        public record Comando(DatosIniciarSesion Datos):IRequest<UsuarioDTO>;

        public class ValidadorComando:AbstractValidator<Comando> {
            public ValidadorComando()
            {
                RuleFor(x => x.Datos.UserName).NotEmpty().NotNull().MinimumLength(4).MaximumLength(25);

                RuleFor(x => x.Datos.Clave)
                    .NotNull()
                    .NotNull();
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
                var usuario = await contexto.Usuario
                    .FirstOrDefaultAsync(x => x.UserName == request.Datos.UserName!.Trim().ToUpper());
                usuario.ThrowIfNull(()=> new ErroresUsuario.CredencialesIncorrectas());
                usuario.Clave.Throw(() => new ErroresUsuario.CredencialesIncorrectas())
                    .IfFalse(x => Criptografia.Verificar(request.Datos.Clave!, usuario.Clave));

                return mapper.Map<UsuarioDTO>(usuario);
            }
        }
    }
}
