using Aplicacion.Dominio.Movimiento;
using Aplicacion.DTOs;
using Aplicacion.Infraestructura.Persistencia;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Caracteristicas.Movimiento
{
    public class ObtenerTodosLosMovimientos
    {
        public record Consulta():IRequest<IReadOnlyCollection<MovimientoDTO>>;
        public class Handler : IRequestHandler<Consulta, IReadOnlyCollection<MovimientoDTO>>
        {
            private readonly Contexto contexto;
            private readonly IMapper mapper;

            public Handler(Contexto contexto, IMapper mapper)
            {
                this.contexto = contexto;
                this.mapper = mapper;
            }
            public async Task<IReadOnlyCollection<MovimientoDTO>> Handle(Consulta request, CancellationToken cancellationToken)
            {
                var movimientos = await contexto.Movimiento
                    .AsNoTracking()
                    .Include(x => x.Billetera)
                    .Where(x => !x.Eliminado)
                    .ToListAsync();

                return mapper.Map<IReadOnlyCollection<MovimientoDTO>>(movimientos);
            }
        }
    }
}
