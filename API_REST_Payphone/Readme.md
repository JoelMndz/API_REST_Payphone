# Proyecto API
- Se debe definir las variables de entorno basandose en .env.example
- Se debe realizar la migracion a la base de datos (por ejemplo Banca_DEV) se puede suar los siguiente comandos 
previamente instalando el cli de EF: dotnet ef update



# Proyecto Test de integracion
- Nesecita un .env.test dentro del proyecto Aplicacion.Integration.Tests
- Se debe realizar la migracion a la base de datos de pruebas (por ejemplo Banca__TEST) con este comando: 
dotnet ef database update --connection "URI_DB_"
- 