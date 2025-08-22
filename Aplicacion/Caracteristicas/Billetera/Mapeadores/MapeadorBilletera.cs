using Aplicacion.Dominio.Billetera;
using Aplicacion.DTOs;
using AutoMapper;
using BilleteraDominio = Aplicacion.Dominio.Billetera.Billetera;

namespace Aplicacion.Caracteristicas.Billetera.Mapeadores
{
    public class MapeadorBilletera:Profile
    {
        public MapeadorBilletera()
        {
            CreateMap<BilleteraDominio, BilleteraDTO>();
        }
    }
}
