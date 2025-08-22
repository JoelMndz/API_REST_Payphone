using Aplicacion.Dominio.Movimiento;
using Aplicacion.DTOs;
using AutoMapper;
using MovimientoDominio = Aplicacion.Dominio.Movimiento.Movimiento;

namespace Aplicacion.Caracteristicas.Movimiento.Mapeadores
{
    public class MapeadorMovimiento:Profile
    {
        public MapeadorMovimiento()
        {
            CreateMap<MovimientoDominio, MovimientoDTO>()
                .ForMember(x => x.DocumentoIdentidad, opt => opt.MapFrom(x => x.Billetera.DocumentoIdentidad))
                .ForMember(x => x.NombrePropietario, opt => opt.MapFrom(x => x.Billetera.NombrePropietario));
        }
    }
}
