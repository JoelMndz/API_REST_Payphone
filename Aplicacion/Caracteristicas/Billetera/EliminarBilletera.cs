using Aplicacion.Dominio.Billetera.Errores;
using Aplicacion.Dominio.Comunes;
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
using Throw;

namespace Aplicacion.Caracteristicas.Billetera
{
    public class EliminarBilletera
    {
        public record Comando(int Id):IRequest<BilleteraDTO>;
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
                var billetera = await contexto.Billetera.FirstOrDefaultAsync(x => x.Id == request.Id && !x.Eliminado);
                billetera.ThrowIfNull(() => new ErroresBilletera.NoExisteBilletera(request.Id));
                billetera.Eliminar();
                (await contexto.SaveChangesAsync())
                    .Throw(() => new Errores.NoSePuedieronGuardarLosCambios()).IfTrue(x => x == 0);

                return mapper.Map<BilleteraDTO>(billetera);
            }
        }
    }
}
