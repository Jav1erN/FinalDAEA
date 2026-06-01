# ClinicSystem Architecture

Este proyecto sigue arquitectura hexagonal con CQRS, MediatR y casos de uso verticales.

- `Domain`: entidades generadas desde la base de datos, sin anotaciones de datos.
- `Application`: casos de uso y puertos. Cada vertical vive en `UseCases/{Feature}` y dentro solo contiene `Commands`, `Queries` y `Dtos`.
- `Infrastructure`: adaptadores EF Core, `ClinicDbContext`, repositorios y `UnitOfWork`.
- `API`: controladores livianos que delegan en MediatR.

Los metodos de los repositorios retornan `Task<IEnumerable<T>>` o `Task<T?>`, nunca `IQueryable`. Las consultas complejas se escriben dentro del repositorio usando LINQ.
