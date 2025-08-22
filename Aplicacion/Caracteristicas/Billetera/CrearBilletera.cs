using Aplicacion.DTOs;
using Aplicacion.Infraestructura.Persistencia;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aplicacion.Caracteristicas.Billetera
{
    public class CrearBilletera
    {
        public class DatosCrearBilletera
        {
            public string? DocumentoIdentidad { get; set; }
            public string? NombrePropietario { get; set; }
        }
        public record Comando(DatosCrearBilletera Datos):IRequest<BilleteraDTO>;
        public class ValidadorComando:AbstractValidator<Comando>
        {
            public ValidadorComando()
            {
                RuleFor(x => x.Datos.DocumentoIdentidad)
                    .NotNull()
                    .Must(x => x == null ? true : ValidarCedula(x))
                    .WithMessage("El documento de identidad(cédula) no tiene el formato correcto");
                RuleFor(x => x.Datos.NombrePropietario)
                    .NotEmpty()
                    .NotNull()
                    .MinimumLength(4)
                    .MaximumLength(100)
                    .Must(x => x == null ? true : Regex.IsMatch(x, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$"))
                    .WithMessage("El nombre solo debe tener letras");
            }
            private bool ValidarCedula(string cedula)
            {
                if (!SoloNumeros(cedula))
                {
                    return false;
                }

                if (cedula.Length != 10)
                {
                    return false;
                }

                List<int> digitos = new();
                for (int i = 0; i < cedula.Length; i++)
                {
                    digitos.Add(int.Parse(cedula[i].ToString()));
                }
                List<int> posicionesImpares = new List<int>();
                List<int> posicionesPares = new List<int>();

                for (int i = 0; i < 9; i++)
                {
                    if (i % 2 == 0)
                    {
                        if ((digitos[i] * 2) > 9)
                        {
                            posicionesImpares.Add((digitos[i] * 2) - 9);
                        }
                        else
                        {
                            posicionesImpares.Add(digitos[i] * 2);
                        }

                    }
                    else
                    {
                        posicionesPares.Add(digitos[i]);
                    }
                }

                int suma = posicionesPares.Sum() + posicionesImpares.Sum();
                int modulo = suma % 10;
                int digitoVerificador = 0;
                if (modulo > 0)
                {
                    digitoVerificador = 10 - modulo;
                }

                if (digitos[9] != digitoVerificador)
                {
                    return false;
                }

                return true;
            }

            private bool SoloNumeros(string cadena)
            {
                for (int i = 0; i < cadena.Length; i++)
                {
                    if (!char.IsNumber(cadena[i]))
                    {
                        return false;
                    }
                }
                return true;
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
            public Task<BilleteraDTO> Handle(Comando request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
