using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCrypt.Net;

namespace Aplicacion.Helper.Comunes
{
    public class Criptografia
    {
        public static string Encriptar(string texto)
        {
            return BCrypt.Net.BCrypt.HashPassword(texto, workFactor: 12);
        }

        public static bool Verificar(string texto, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(texto, hash);
        }
    }
}
