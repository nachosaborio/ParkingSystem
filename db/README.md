# Base de datos

Parking System usa SQLite como base de datos. Este directorio no contiene scripts SQL: el archivo de base de datos `parking.db` se genera automáticamente al arrancar la API.

## Cómo se genera

- Al iniciar, la API llama a `EnsureCreated()` de EF Core, que crea el esquema (tablas `Empleado`, `Parqueo` y `Tiquete`) si la base de datos no existe.
- La cadena de conexión es `Data Source=parking.db`, relativa al directorio de trabajo. Al ejecutar `dotnet run` desde `src/Parking.Api`, el archivo se crea en `src/Parking.Api/parking.db`.

## Control de versiones

`parking.db` (y sus archivos auxiliares `*.db-shm` y `*.db-wal`) está ignorado por git, por lo que nunca se versiona.

## Regenerar la base de datos

1. Detén la API.
2. Borra `src/Parking.Api/parking.db` (y los archivos `parking.db-shm` / `parking.db-wal` si existen).
3. Vuelve a arrancar la API: se creará una base de datos nueva con el esquema vacío.
