# Informe tecnico final - ClinicSystem

## 1. Objetivo del proyecto

ClinicSystem es una API backend en .NET 9 para la gestion de un sistema clinico, construida sobre una base de datos PostgreSQL llamada `clinic_system`.

El objetivo principal fue transformar el proyecto inicial en una solucion profesional con:

- Arquitectura hexagonal.
- Separacion por capas.
- CQRS.
- MediatR.
- Unit of Work.
- Repositorios especificos.
- DTOs centralizados.
- Controladores livianos.
- Reglas de negocio fuera de la API.
- Validaciones con FluentValidation.
- Pruebas iniciales.
- Preparacion para validacion funcional mediante Swagger.

El proyecto no esta enfocado como una aplicacion monolitica con logica directa en controladores. Esta orientado a una arquitectura limpia donde la API solo recibe solicitudes HTTP y delega el flujo a casos de uso.

## 2. Estado general actual

La solucion compila correctamente.

Validaciones ejecutadas:

```bash
dotnet build ClinicSystem.sln
dotnet test ClinicSystem.sln --no-build
```

Resultado:

- Build correcto.
- 0 errores.
- 0 warnings.
- 3 pruebas unitarias correctas.
- API inicia correctamente.
- Swagger queda disponible en ambiente Development.

La aplicacion esta lista para iniciar pruebas funcionales mediante Swagger contra la base de datos `clinic_system`.

Importante: lista para pruebas funcionales no significa lista para produccion. Para produccion faltarian autenticacion real aplicada a endpoints, politicas de autorizacion, pruebas de integracion completas, observabilidad, hardening de seguridad y validacion exhaustiva de reglas clinicas.

## 3. Estructura de la solucion

La solucion contiene cinco proyectos:

```text
ClinicSystem.sln
|
|-- ClinicSystem.API
|-- ClinicSystem.Application
|-- ClinicSystem.Domain
|-- ClinicSystem.Infrastructure
|-- ClinicSystem.Tests
```

### 3.1 ClinicSystem.API

Capa de entrada HTTP.

Responsabilidades:

- Registrar servicios de Application e Infrastructure.
- Exponer endpoints REST.
- Configurar Swagger.
- Configurar manejo global de errores.
- Recibir requests HTTP.
- Enviar commands y queries mediante MediatR.

No debe contener:

- Logica de negocio.
- Consultas directas con Entity Framework.
- Acceso directo a `DbContext`.
- Reglas clinicas.

Los controladores son livianos. Ejemplo de flujo:

```text
HTTP Request
-> Controller
-> ISender.Send(command/query)
-> Handler en Application
-> Puerto de repositorio / servicio
-> Adaptador en Infrastructure
-> DbContext
```

### 3.2 ClinicSystem.Application

Capa de casos de uso.

Responsabilidades:

- Organizar modulos verticales.
- Definir commands.
- Definir queries.
- Implementar handlers.
- Usar MediatR.
- Usar DTOs.
- Aplicar validaciones con FluentValidation.
- Consumir puertos definidos en Domain.

Estructura principal:

```text
ClinicSystem.Application
|
|-- Common
|   |-- Behaviors
|   |-- Dtos
|   |-- Models
|
|-- UseCases
    |-- Appointments
    |   |-- Commands
    |   |-- Queries
    |
    |-- Patients
    |   |-- Commands
    |   |-- Queries
    |
    |-- ...
```

Cada modulo dentro de `UseCases` contiene solo:

- `Commands`
- `Queries`

Los DTOs ya no estan dentro de cada use case. Fueron centralizados en:

```text
ClinicSystem.Application/Common/Dtos
```

Esto permite mantener un unico punto de contratos de salida y mappings.

### 3.3 ClinicSystem.Domain

Capa de dominio.

Responsabilidades:

- Contener entidades scaffolded desde la base de datos.
- Definir puertos de repositorios.
- Definir puertos de servicios de negocio.
- Definir modelos comunes de dominio.

Estructura relevante:

```text
ClinicSystem.Domain
|
|-- Common
|   |-- BusinessRuleResult.cs
|
|-- Entities
|
|-- Ports
    |-- Repositories
    |-- Services
```

Las entidades generadas desde la base de datos estan en:

```text
ClinicSystem.Domain/Entities
```

Estas entidades no contienen anotaciones de datos. El mapeo de base de datos vive en Infrastructure, dentro de `ClinicDbContext`.

### 3.4 ClinicSystem.Infrastructure

Capa de adaptadores.

Responsabilidades:

- Implementar repositorios.
- Implementar Unit of Work.
- Implementar servicios de negocio que necesitan infraestructura.
- Configurar Entity Framework Core.
- Contener `ClinicDbContext`.
- Registrar dependencias.

Estructura relevante:

```text
ClinicSystem.Infrastructure
|
|-- Adapters
|   |-- Repositories
|   |-- Services
|
|-- Persistence
    |-- Context
    |-- UnitOfWork.cs
```

### 3.5 ClinicSystem.Tests

Proyecto de pruebas unitarias.

Responsabilidades:

- Validar reglas de negocio aisladas.
- Dar una base inicial para pruebas automatizadas.

Actualmente contiene pruebas para:

- `BillingPolicyService`

Resultado actual:

```text
Total: 3
Passed: 3
Failed: 0
Skipped: 0
```

## 4. Arquitectura hexagonal aplicada

La arquitectura hexagonal se implemento separando:

- Dominio.
- Puertos.
- Adaptadores.
- Casos de uso.
- Entrada HTTP.

### 4.1 Puertos de repositorio

Ubicacion:

```text
ClinicSystem.Domain/Ports/Repositories
```

Ejemplo:

```csharp
public interface IPatientRepository : IRepository<Patient>
{
    Task<Patient?> GetByDocumentNumberAsync(
        string documentNumber,
        CancellationToken cancellationToken = default);
}
```

Regla importante:

Los repositorios no exponen `IQueryable`.

La interfaz base es:

```csharp
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken = default);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity);
    void Remove(T entity);
}
```

Esto cumple la regla solicitada:

```text
Los metodos de los repositorios retornan Task<IEnumerable<T>> o Task<T?>, nunca IQueryable.
Las consultas complejas se escriben dentro del repositorio usando LINQ.
```

### 4.2 Adaptadores de repositorio

Ubicacion:

```text
ClinicSystem.Infrastructure/Adapters/Repositories
```

Los adaptadores implementan los puertos del dominio usando Entity Framework Core.

Ejemplo:

```csharp
public class PatientRepository : Repository<Patient>, IPatientRepository
{
    public PatientRepository(ClinicDbContext context) : base(context)
    {
    }

    public async Task<Patient?> GetByDocumentNumberAsync(
        string documentNumber,
        CancellationToken cancellationToken = default)
    {
        return await Context.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.DocumentNumber == documentNumber, cancellationToken);
    }
}
```

Entity Framework queda encapsulado en Infrastructure.

### 4.3 Puertos de servicios

Ubicacion:

```text
ClinicSystem.Domain/Ports/Services
```

Servicios creados:

- `IAppointmentSchedulingService`
- `IBillingPolicyService`
- `IInventoryPolicyService`

Estos puertos representan reglas de negocio que no deben vivir en controladores.

### 4.4 Adaptadores de servicios

Ubicacion:

```text
ClinicSystem.Infrastructure/Adapters/Services
```

Implementaciones actuales:

- `AppointmentSchedulingService`
- `BillingPolicyService`
- `InventoryPolicyService`

Estos servicios aplican reglas fuertes:

- Evitar cruce de citas por doctor.
- Validar montos de facturacion.
- Validar movimientos de inventario contra stock disponible.

## 5. CQRS y MediatR

Cada modulo tiene commands y queries separados.

Ejemplo:

```text
ClinicSystem.Application/UseCases/Appointments
|
|-- Commands
|   |-- CreateAppointmentCommand.cs
|   |-- UpdateAppointmentCommand.cs
|   |-- DeleteAppointmentCommand.cs
|   |-- AppointmentValidators.cs
|
|-- Queries
    |-- GetAppointmentByIdQuery.cs
    |-- GetAppointmentsQuery.cs
```

Los handlers estan dentro de los archivos de commands o queries segun corresponda.

Los controladores no llaman a repositorios directamente. Usan:

```csharp
private readonly ISender _sender;
```

Y ejecutan:

```csharp
await _sender.Send(command, cancellationToken);
```

## 6. Modulos implementados

Se generaron 28 modulos, equivalentes a las entidades reales del `ClinicDbContext`.

Modulos:

- Appointments
- AppointmentStatuses
- AuditLogs
- BillingDetails
- Billings
- Departments
- Diagnoses
- Doctors
- InsuranceCompanies
- InsurancePolicies
- LaboratoryResults
- LaboratoryTests
- MedicalRecords
- Medications
- Notifications
- NotificationTypes
- Patients
- Payments
- Permissions
- PrescriptionDetails
- Prescriptions
- RefreshTokens
- Roles
- Specialties
- StockMovements
- Treatments
- Users
- VitalSigns

Cada modulo tiene:

- Create command.
- Update command.
- Delete command.
- Get by id query.
- Get all query.
- Validators donde corresponde.
- Controller REST.

## 7. DTOs centralizados

Los DTOs se encuentran en:

```text
ClinicSystem.Application/Common/Dtos
```

Cantidad actual:

- 57 archivos entre DTOs y mappings.

Esto incluye:

- DTOs de salida.
- Extension methods `ToDto`.
- Mappings entidad -> DTO.

Ejemplo:

```csharp
public static class PatientMappings
{
    public static PatientDto ToDto(this Patient entity)
    {
        return new PatientDto
        {
            PatientId = entity.PatientId,
            DocumentNumber = entity.DocumentNumber,
            FirstName = entity.FirstName,
            LastName = entity.LastName
        };
    }
}
```

## 8. Commands con getters y setters

Todos los commands fueron convertidos a clases con propiedades publicas.

Ejemplo:

```csharp
public class CreatePatientCommand : IRequest<Result<PatientDto>>
{
    public Guid? UserId { get; set; }

    public string DocumentNumber { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
}
```

Esto facilita:

- Binding desde JSON en ASP.NET Core.
- Uso desde Swagger.
- Validacion con FluentValidation.
- Compatibilidad con formularios y clientes HTTP.

## 9. Unit of Work

Ubicacion del puerto:

```text
ClinicSystem.Domain/Ports/Repositories/IUnitOfWork.cs
```

Ubicacion de implementacion:

```text
ClinicSystem.Infrastructure/Persistence/UnitOfWork.cs
```

El Unit of Work expone repositorios especificos:

```csharp
IAppointmentRepository Appointments { get; }
IPatientRepository Patients { get; }
IMedicationRepository Medications { get; }
IBillingRepository Billings { get; }
...
```

Tambien contiene:

```csharp
Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
```

Los handlers usan el Unit of Work para persistir cambios.

## 10. Reglas de negocio implementadas

### 10.1 Agenda medica

Servicio:

```text
IAppointmentSchedulingService
AppointmentSchedulingService
```

Regla:

Un doctor no puede tener dos citas que se crucen en el mismo rango horario.

Se usa en:

- `CreateAppointmentCommandHandler`
- `UpdateAppointmentCommandHandler`

### 10.2 Facturacion

Servicio:

```text
IBillingPolicyService
BillingPolicyService
```

Reglas:

- El subtotal no puede ser negativo.
- El descuento no puede ser negativo.
- La cobertura del seguro no puede ser negativa.
- Descuento + cobertura no pueden superar el subtotal.
- El total se calcula como:

```text
subtotal - discount - insuranceCoverage
```

Se usa en:

- `CreateBillingCommandHandler`
- `UpdateBillingCommandHandler`

### 10.3 Inventario

Servicio:

```text
IInventoryPolicyService
InventoryPolicyService
```

Reglas:

- La cantidad de movimiento no puede ser cero.
- El medicamento debe existir.
- Para salidas de inventario no se puede superar el stock disponible.

Se usa en:

- `CreateStockMovementCommandHandler`
- `UpdateStockMovementCommandHandler`

## 11. Validaciones

Se usa FluentValidation.

Registro:

```csharp
services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
```

Esto permite que cada command sea validado antes de llegar al handler.

Ejemplo:

```csharp
public class CreatePatientValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientValidator()
    {
        RuleFor(x => x.DocumentNumber).NotEmpty().MaximumLength(20);
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email));
    }
}
```

## 12. Manejo de errores

La API tiene manejo global de errores en `Program.cs`.

Para errores de validacion:

- HTTP 400.
- Respuesta JSON con detalle por propiedad.

Para errores inesperados:

- HTTP 500.
- Respuesta generica.

Esto evita exponer detalles internos del servidor.

## 13. Swagger

Swagger esta configurado en `ClinicSystem.API`.

En ambiente Development:

```csharp
app.UseSwagger();
app.UseSwaggerUI();
```

Para lanzar la API:

```bash
dotnet run --project ClinicSystem.API
```

Luego abrir:

```text
https://localhost:{puerto}/swagger
```

O si se usa HTTP:

```bash
dotnet run --project ClinicSystem.API --urls http://localhost:5092
```

Y abrir:

```text
http://localhost:5092/swagger
```

## 14. Pruebas recomendadas por Swagger

Orden recomendado:

1. Probar endpoints catalogo/base:
   - `Departments`
   - `Roles`
   - `Permissions`
   - `AppointmentStatuses`
   - `InsuranceCompanies`
   - `Medications`

2. Probar entidades principales:
   - `Users`
   - `Patients`
   - `Doctors`
   - `Appointments`

3. Probar flujo clinico:
   - Crear paciente.
   - Crear doctor.
   - Crear cita.
   - Intentar crear cita cruzada para el mismo doctor.
   - Confirmar que la regla de negocio bloquea el cruce.

4. Probar facturacion:
   - Crear billing valido.
   - Crear billing con descuento mayor al subtotal.
   - Confirmar que se rechaza.

5. Probar inventario:
   - Crear movimiento con cantidad 0.
   - Crear salida mayor al stock.
   - Confirmar que se rechaza.

6. Probar operaciones CRUD generales:
   - GET all.
   - GET by id.
   - POST.
   - PUT.
   - DELETE.

## 15. Consideraciones importantes para pruebas

La API esta lista para pruebas mediante Swagger, pero hay condiciones:

### 15.1 Base de datos

Debe existir la base:

```text
clinic_system
```

La cadena actual esta en:

```text
ClinicSystem.API/appsettings.json
```

Ejemplo:

```json
"DefaultConnection": "Host=localhost;Port=5432;Database=clinic_system;Username=postgres;Password=123456"
```

### 15.2 Llaves foraneas

Muchos endpoints requieren IDs existentes.

Ejemplo:

Para crear una cita se requieren:

- `PatientId`
- `DoctorId`
- `StatusId`

Estos valores deben existir previamente en base de datos.

### 15.3 Orden de insercion

No todos los modulos se pueden probar en cualquier orden por las relaciones de la base.

Ejemplo:

- No se debe crear `Doctor` sin `User` y `Specialty`.
- No se debe crear `Specialty` sin `Department`.
- No se debe crear `Billing` sin `Patient`.
- No se debe crear `StockMovement` sin `Medication`.

### 15.4 Deletes

Los deletes actuales son fisicos.

Esto significa que se ejecuta `Remove`.

Si se desea soft delete usando campos `DeletedAt`, deberia implementarse una regla adicional por modulo.

## 16. Lo que esta bien enfocado

El proyecto esta bien enfocado para una entrega backend profesional de arquitectura.

Puntos fuertes:

- Separacion clara de responsabilidades.
- Controladores livianos.
- Casos de uso verticales.
- MediatR aplicado.
- Repositorios encapsulados.
- Unit of Work completo.
- DTOs centralizados.
- Validaciones automatizadas.
- Servicios de reglas de negocio.
- Pruebas unitarias iniciales.
- Swagger listo para validar.
- EF Core aislado en Infrastructure.
- Domain sin dependencia de Infrastructure.

## 17. Lo que no queda al aire

No queda al aire lo siguiente:

- Estructura de capas.
- Direccion de dependencias.
- Ubicacion de DTOs.
- Ubicacion de commands y queries.
- Ubicacion de handlers.
- Registro de MediatR.
- Registro de FluentValidation.
- Registro de DbContext.
- Registro de repositorios.
- Registro de UnitOfWork.
- Registro de servicios de negocio.
- Scaffolding de entidades.
- Mapeo EF en Infrastructure.
- Endpoints CRUD por modulo.
- Build y test base.

## 18. Lo que aun debe considerarse antes de produccion

Aunque esta listo para pruebas por Swagger, no se recomienda considerarlo listo para produccion sin:

- Autenticacion y autorizacion aplicadas a endpoints.
- JWT funcional conectado a usuarios reales.
- Politicas por rol.
- Soft delete si el negocio lo exige.
- Paginacion en consultas `GET all`.
- Filtros por estado, fecha, paciente, doctor, etc.
- Pruebas de integracion con PostgreSQL real.
- Pruebas end-to-end.
- Logging estructurado por request.
- Auditoria real.
- Manejo de concurrencia.
- Transacciones explicitas en flujos complejos.
- Reglas clinicas adicionales.
- Seguridad de secretos fuera de `appsettings.json`.
- Versionado de API.

## 19. Comandos de validacion

Restaurar paquetes:

```bash
dotnet restore ClinicSystem.sln
```

Compilar:

```bash
dotnet build ClinicSystem.sln
```

Ejecutar pruebas:

```bash
dotnet test ClinicSystem.sln
```

Levantar API:

```bash
dotnet run --project ClinicSystem.API
```

Levantar API en puerto fijo:

```bash
dotnet run --project ClinicSystem.API --urls http://localhost:5092
```

## 20. Dictamen tecnico final

El proyecto esta correctamente enfocado.

La arquitectura solicitada fue aplicada de forma consistente:

- Hexagonal.
- CQRS.
- MediatR.
- Ports and Adapters.
- Unit of Work.
- Repositorios especificos.
- DTOs centralizados.
- Modulos verticales.
- Controladores livianos.
- Servicios de reglas de negocio.
- Validaciones.
- Pruebas iniciales.

El sistema esta listo para lanzarse a una fase de pruebas funcionales mediante Swagger.

La fase recomendada ahora es validar modulo por modulo contra la base `clinic_system`, empezando por catalogos y luego flujos dependientes.

No se recomienda venderlo como produccion final todavia, pero si como backend academico/profesional listo para validacion funcional y demostracion tecnica.
