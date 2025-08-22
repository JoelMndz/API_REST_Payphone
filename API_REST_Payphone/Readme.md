# Gestion de billeteras
La prueba técnica es una API REST la cual sirve para la gestion de billeteras y cuenta con autenticación(JWT).

## Arquitectura propuesta

```
.
├── tests/                  
│   ├─- Aplicacion.Integration.Tests/           # Test de integracion afecto la DB de TEST
│   ├── Aplicacion.Shared.Test/					# Su finalidad es tener objetos factorys
│   ├── Aplicacion.Unit.Test/					# Test unitarios sobre el dominio y validaciones de la características
├── API_REST_Payphone							# Api Rest (Controladores)
├── Aplicacion
	├─- Caracteristicas							# Aqui esta los casos de uso por dominio (Implementamos Veritcal slice)
	├─- Dominio									# Entidades del dominio
	├─- DTOs									# Todos los objetos DTO que se comparten entre diferentes casos de uso
	├─- Helper									# Hay objetos para definir comportamiento, validaciones, interfaces
	├─- Infraestructura							# Base de datos, configuracion de EF

 ````
## Puntos más relevantes del Stack
- .NET 8
- Arquitectura Hexagonal + Vertical slice + CQRS
- Librerias: MediatR, FluentValidation, Automapper, EF (Code firts)
- Autenticacion con JWT
- Test de integracion y unitarios
- Sql Server

# Para levantar el proyecto seguir estas indicaciones

### Proyecto API
- En el archivo .env ubicar su cadena de conexion de SQL Server en la propiedad URI_DB
- Si desea puede cambiar el valor SECRETO_JWT
- La contruccion de la base de datos la realiza automaticamente al levantar el API
- Cuando ejecuta el proyecto puede interactuar mediante swagger con el API

### Aplicacion.Integration.Tests
- En el archivo .env.test ubicar su cadena de conexion de SQL Server(Esta db debe ser diferente que la del API) en la propiedad URI_DB
- Aqui si debe realizar la migracion con el siguiente comando:
```bash
dotnet tool install --global dotnet-ef
dotnet ef database update --connection "Aqui va la cadena de la conexion de la DB de pruebas"
```
- Listo Ahora ya puede ejecutar los test de integración