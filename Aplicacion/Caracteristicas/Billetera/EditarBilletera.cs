using Aplicacion.Dominio.Billetera.Enums;
using Aplicacion.DTOs;
using FluentValidation;
using MediatR;
using BilleteraDominio = Aplicacion.Dominio.Billetera.Billetera;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Aplicacion.Infraestructura.Persistencia;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Throw;
using Aplicacion.Dominio.Billetera.Errores;
using Aplicacion.Dominio.Comunes;

namespace Aplicacion.Caracteristicas.Billetera
{
    public class EditarBilletera
    {
        public class DatosEditarBilletera
        {
            public int? Id { get; set; }
            public string? NombrePropietario { get; set; }
            public EstadosBilletera? Estado { get; set; }
        }
        public record Comando(DatosEditarBilletera Datos):IRequest<BilleteraDTO>;
        public class ValidadorComando:AbstractValidator<Comando>
        {
            public ValidadorComando()
            {
                RuleFor(x => x.Datos.Id).NotNull();

                RuleFor(x => x.Datos.NombrePropietario)
                    .NotEmpty()
                    .NotNull()
                    .MinimumLength(4)
                    .MaximumLength(100)
                    .Must(x => x == null ? true : Regex.IsMatch(x, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$"))
                    .WithMessage("El nombre solo debe tener letras");
                RuleFor(x => x.Datos.Estado)
                    .NotNull()
                    .IsInEnum();
            }
        }
        public class Handler : IRequestHandler<Comando, BilleteraDTO>
        {
            private readonly Contexto contexto;
            private readonly IMapper mapper;

            public Handler(Contexto contexto, IMapper mapper)
            {
                this.contexto = contexto;
                this.mapper = mapper;
            }
            public async Task<BilleteraDTO> Handle(Comando request, CancellationToken cancellationToken)
            {
                var billetera = await contexto.Billetera.FirstOrDefaultAsync(x => x.Id == request.Datos.Id && !x.Eliminado);
                billetera.ThrowIfNull(() => new ErroresBilletera.NoExisteBilletera(request.Datos.Id!.Value));
                billetera.Editar(request.Datos.NombrePropietario!, request.Datos.Estado!.Value);
                (await contexto.SaveChangesAsync())
                    .Throw(() => new Errores.NoSePuedieronGuardarLosCambios()).IfTrue(x => x == 0);

                return mapper.Map<BilleteraDTO>(billetera);
            }
        }
    }
}
