using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Domain.Ports.Repositories;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<Department?> GetByNameAsync(
        string name,
        CancellationToken cancellationToken = default);
}