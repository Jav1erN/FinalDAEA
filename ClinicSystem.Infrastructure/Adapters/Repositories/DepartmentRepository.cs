using ClinicSystem.Domain.Ports.Repositories;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ClinicSystem.Infrastructure.Adapters.Repositories;

public class DepartmentRepository : Repository<Department>, IDepartmentRepository
{
    public DepartmentRepository(ClinicDbContext context) : base(context)
    {
    }
    
    public async Task<Department?> GetByNameAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        return await Context.Departments
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }
}