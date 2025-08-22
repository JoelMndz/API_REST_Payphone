# Aplicacion
Este proyecto de los desarrollo usando un MIX entre arquitectura hexognal, vertical slice, CQRS y MediatR.
Tambien se implementaron interceptores para controlar las modificaciones.
- Se implemento TDD, DDD, Test de integracion simulando las operaciones en la DB
- Para garantizar que el Dominio prevalezca se utilizo EF haciendo Code Firts.

# Para levantar el proyecto seguir estas indicaciones

## Proyecto API
- Se debe definir las variables de entorno basandose en .env.example
- Se debe realizar la migracion a la base de datos (por ejemplo Banca_DEV) se puede usar los siguiente comandos 
previamente instalando el cli de EF: dotnet ef update


## Proyecto Test de integracion
- Nesecita un .env.test dentro del proyecto Aplicacion.Integration.Tests
- Se debe realizar la migracion a la base de datos de pruebas (por ejemplo Banca__TEST) con este comando: 
dotnet ef database update --connection "URI_DB_"
- 
