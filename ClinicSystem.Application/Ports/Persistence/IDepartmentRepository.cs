using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.Ports.Persistence;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<Department?> GetByNameAsync(
        string name,
        CancellationToken cancellationToken = default);
}