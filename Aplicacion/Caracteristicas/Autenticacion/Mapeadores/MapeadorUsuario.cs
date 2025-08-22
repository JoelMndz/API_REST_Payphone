using Aplicacion.Dominio.Usuario;
using Aplicacion.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Caracteristicas.Autenticacion.Mapeadores
{
    public class MapeadorUsuario:Profile
    {
        public MapeadorUsuario()
        {
            CreateMap<Usuario, UsuarioDTO>();
        }
    }
}
