using Aplicacion.Dominio.Billetera.Errores;
using Aplicacion.Dominio.Movimiento.Enums;
using Aplicacion.DTOs;
using Aplicacion.Infraestructura.Persistencia;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovimientoDominio = Aplicacion.Dominio.Movimiento.Movimiento;
using Throw;
using Aplicacion.Dominio.Comunes;
using Aplicacion.Dominio.Movimiento.Errores;
using Aplicacion.Dominio.Billetera.Enums;

namespace Aplicacion.Caracteristicas.Movimiento
{
    public class RealizarMovimiento
    {
        public class DatosRealizarMovimiento
        {
            public TiposDeOperacion? Tipo { get; set; }
            public double? Cantidad { get; set; }
            public int? IdBilletera { get; set; }
        }
        public record Comando(DatosRealizarMovimiento Datos):IRequest<MovimientoDTO>;
        public class ValidadorComando:AbstractValidator<Comando>
        {
            public ValidadorComando()
            {
                RuleFor(x => x.Datos.Tipo).NotNull().IsInEnum();
                RuleFor(x => x.Datos.Cantidad).NotNull().GreaterThan(0);
                RuleFor(x => x.Datos.IdBilletera).NotNull();
            }
        }
        public class Handler : IRequestHandler<Comando, MovimientoDTO>
        {
            private readonly Contexto contexto;
            private readonly IMapper mapper;

            public Handler(Contexto contexto, IMapper mapper)
            {
                this.contexto = contexto;
                this.mapper = mapper;
            }
            public async Task<MovimientoDTO> Handle(Comando request, CancellationToken cancellationToken)
            {
                var billetera = await contexto.Billetera
                    .FirstOrDefaultAsync(x => x.Id == request.Datos.IdBilletera!.Value && !x.Eliminado);
                billetera.ThrowIfNull(() => new ErroresBilletera.NoExisteBilletera(request.Datos.IdBilletera!.Value));
                var movimiento = MovimientoDominio.Crear(request.Datos.Tipo!.Value, request.Datos.Cantidad!.Value, billetera);
                
                billetera.Estado.Throw(()=> new ErroresMovimiento.LaCuentaNoEstaActiva())
                    .IfTrue(x => x != EstadosBilletera.Activa);
                if(movimiento.Tipo == TiposDeOperacion.Debito)
                {
                    if (billetera.SaldoActual < movimiento.Cantidad)
                        throw new ErroresMovimiento.SaldoInsuficiente(billetera.SaldoActual);
                    billetera.SaldoActual -= movimiento.Cantidad;
                }
                else
                {
                    billetera.SaldoActual += movimiento.Cantidad;
                }
                
                contexto.Movimiento.Add(movimiento);
                (await contexto.SaveChangesAsync())
                    .Throw(() => new Errores.NoSePuedieronGuardarLosCambios()).IfTrue(x => x == 0);

                return mapper.Map<MovimientoDTO>(movimiento);
            }
            
        }
    }
}
