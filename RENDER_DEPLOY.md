# Despliegue de ClinicSystem Backend en Render

## Objetivo

Este documento describe la preparacion tecnica del backend `ClinicSystem` para publicarlo en Render como Web Service Docker, conectado a PostgreSQL y validable mediante Swagger.

## Cambios aplicados para Render

- Se agrego `Dockerfile` multi-stage para compilar y publicar `ClinicSystem.API` en modo `Release`.
- Se agrego `.dockerignore` para evitar enviar artefactos locales, carpetas `bin/obj`, configuraciones de IDE y metadatos Git al contexto Docker.
- Se agrego `render.yaml` para permitir despliegue por Blueprint desde la raiz del repositorio.
- Se preparo `ClinicSystem.API/Program.cs` para escuchar el puerto entregado por `PORT`.
- Se agrego endpoint `GET /health` para health checks HTTP.
- Se dejo Swagger habilitable por variable `ENABLE_SWAGGER=true`.
- Se evito `UseHttpsRedirection` en Production porque Render termina TLS en su balanceador y reenvia trafico HTTP al contenedor.
- Se preparo `ClinicSystem.Infrastructure` para leer `ConnectionStrings:DefaultConnection` o `DATABASE_URL`.

## Arquitectura conservada

El despliegue no modifica la arquitectura hexagonal ni CQRS del sistema:

- `ClinicSystem.Domain` mantiene entidades, puertos y servicios de dominio.
- `ClinicSystem.Application` mantiene casos de uso, comandos, queries, DTOs, validaciones y handlers con MediatR.
- `ClinicSystem.Infrastructure` mantiene adaptadores, persistencia, repositorios, `DbContext` y Unit of Work.
- `ClinicSystem.API` mantiene controladores ligeros, configuracion HTTP y composicion de dependencias.

## Variables de entorno requeridas

En Render se deben configurar estas variables:

| Variable | Valor recomendado | Uso |
| --- | --- | --- |
| `ASPNETCORE_ENVIRONMENT` | `Production` | Ejecuta la API como entorno productivo. |
| `DATABASE_URL` | Valor generado por Render PostgreSQL | Conexion principal a PostgreSQL. |
| `ENABLE_SWAGGER` | `true` para pruebas, `false` al cerrar validacion publica | Habilita `/swagger` fuera de Development. |

Tambien se puede usar `ConnectionStrings__DefaultConnection` en lugar de `DATABASE_URL` si se prefiere entregar una cadena Npgsql formal:

```text
Host=<host>;Port=5432;Database=clinic_system;Username=<user>;Password=<password>;SSL Mode=Require;Trust Server Certificate=true
```

## Base de datos

El proyecto es database-first: las entidades y el `DbContext` vienen de scaffolding sobre la base `clinic_system`.

Render creara una base PostgreSQL vacia si se usa el `render.yaml`. Antes de validar los endpoints funcionales, se debe cargar el esquema y datos requeridos de `clinic_system` en la base de Render.

Opciones recomendadas:

1. Restaurar un dump SQL de `clinic_system` en Render PostgreSQL.
2. Ejecutar scripts SQL de creacion de tablas, relaciones, indices y datos base.
3. Agregar migraciones EF Core en una etapa posterior si se decide pasar de database-first puro a gestion versionada por migraciones.

No se configuro ejecucion automatica de migraciones en arranque porque el sistema fue solicitado como database-first y depende del esquema existente.

## Despliegue por Blueprint

1. Subir el repositorio a GitHub, GitLab o Bitbucket.
2. En Render, crear un nuevo Blueprint.
3. Seleccionar el repositorio.
4. Render detectara `render.yaml` en la raiz.
5. Confirmar la creacion de:
   - Web Service `clinicsystem-api`.
   - PostgreSQL `clinicsystem-db`.
6. Esperar el build Docker y el primer deploy.
7. Cargar el esquema de `clinic_system` en PostgreSQL de Render.
8. Validar:
   - `https://<servicio>.onrender.com/health`
   - `https://<servicio>.onrender.com/swagger`

## Despliegue manual

Si no se usa Blueprint:

1. Crear un PostgreSQL en Render.
2. Crear un Web Service.
3. Seleccionar Docker como runtime.
4. Usar `Dockerfile` en la raiz del repositorio.
5. Configurar Health Check Path como `/health`.
6. Agregar variables:
   - `ASPNETCORE_ENVIRONMENT=Production`
   - `DATABASE_URL=<connection string de Render PostgreSQL>`
   - `ENABLE_SWAGGER=true`
7. Desplegar.
8. Cargar la base de datos `clinic_system`.

## Validacion esperada

### Health check

```http
GET /health
```

Respuesta esperada:

```json
{
  "status": "ok",
  "service": "ClinicSystem.API",
  "environment": "Production",
  "timestamp": "2026-07-01T00:00:00+00:00"
}
```

### Swagger

```http
GET /swagger
```

Debe cargar la documentacion OpenAPI para probar controladores y endpoints.

## Checklist antes de pruebas funcionales

- El build local debe compilar sin errores.
- Las pruebas automatizadas deben pasar.
- El Web Service debe estar en estado `Live` en Render.
- `/health` debe responder HTTP 200.
- `/swagger` debe abrir si `ENABLE_SWAGGER=true`.
- La base `clinic_system` debe tener tablas, claves foraneas y datos base.
- La cadena de conexion debe apuntar a Render PostgreSQL, no a `localhost`.

## Consideraciones de seguridad

- Desactivar Swagger publico al finalizar pruebas externas usando `ENABLE_SWAGGER=false`.
- No versionar credenciales reales.
- Usar variables de entorno de Render para secretos.
- Mantener `ASPNETCORE_ENVIRONMENT=Production` en Render.

## Comandos locales utiles

```powershell
dotnet build ClinicSystem.sln
dotnet test ClinicSystem.sln --no-build
docker build -t clinicsystem-api .
```

Para probar la API local simulando Render:

```powershell
$env:PORT="10000"
$env:ENABLE_SWAGGER="true"
$env:ASPNETCORE_ENVIRONMENT="Production"
dotnet run --project ClinicSystem.API
```

Luego abrir:

```text
http://localhost:10000/health
http://localhost:10000/swagger
```

## Estado para lanzamiento

El backend queda preparado para deploy en Render. El punto critico pendiente no es de codigo: se debe cargar la base de datos real `clinic_system` en PostgreSQL de Render antes de ejecutar pruebas funcionales completas sobre los modulos.
