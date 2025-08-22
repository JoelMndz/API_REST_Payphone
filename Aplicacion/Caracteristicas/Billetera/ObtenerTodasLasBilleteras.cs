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

namespace Aplicacion.Caracteristicas.Billetera
{
    public class ObtenerTodasLasBilleteras
    {
        public class Consulta: IRequest<IReadOnlyCollection<BilleteraDTO>>;

        public class Handler : IRequestHandler<Consulta, IReadOnlyCollection<BilleteraDTO>>
        {
            private readonly Contexto contexto;
            private readonly IMapper mapper;

            public Handler(Contexto contexto, IMapper mapper)
            {
                this.contexto = contexto;
                this.mapper = mapper;
            }
            public async Task<IReadOnlyCollection<BilleteraDTO>> Handle(Consulta request, CancellationToken cancellationToken)
            {
                var billeteras = await contexto.Billetera
                    .AsNoTracking()
                    .Where(x => !x.Eliminado)
                    .ToListAsync();

                return mapper.Map<IReadOnlyCollection<BilleteraDTO>>(billeteras);
            }
        }
    }
}
