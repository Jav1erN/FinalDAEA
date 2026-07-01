# ClinicSystem Architecture

Este proyecto sigue arquitectura hexagonal con CQRS, MediatR y casos de uso verticales.

- `Domain`: entidades generadas desde la base de datos, sin anotaciones de datos.
- `Application`: casos de uso y puertos. Cada vertical vive en `UseCases/{Feature}` y dentro solo contiene `Commands`, `Queries` y `Dtos`.
- `Infrastructure`: adaptadores EF Core, `ClinicDbContext`, repositorios y `UnitOfWork`.
- `API`: controladores livianos que delegan en MediatR.

Los metodos de los repositorios retornan `Task<IEnumerable<T>>` o `Task<T?>`, nunca `IQueryable`. Las consultas complejas se escriben dentro del repositorio usando LINQ.

## Service Ports and Adapters

- `ClinicSystem.Domain/Ports/Services`: contratos de reglas de negocio que deben permanecer independientes de EF Core y de la API.
- `ClinicSystem.Infrastructure/Adapters/Services`: implementaciones concretas de esos contratos cuando necesitan consultar persistencia, fecha/hora, integraciones o infraestructura.

Servicios actuales:

- `IAppointmentSchedulingService`: evita cruces de horarios para un doctor.
- `IBillingPolicyService`: valida montos de facturacion y calcula totales.
- `IInventoryPolicyService`: valida movimientos de inventario contra stock disponible.

Los use cases consumen estos puertos junto con `IUnitOfWork`, manteniendo controladores livianos y reglas de negocio fuera de la API.
