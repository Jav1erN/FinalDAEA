using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;

namespace ClinicSystem.Domain.Ports.Repositories;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<Department?> GetByNameAsync(
        string name,
        CancellationToken cancellationToken = default);
}