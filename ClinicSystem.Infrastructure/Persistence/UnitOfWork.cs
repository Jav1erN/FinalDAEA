using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Infrastructure.Persistence.Context;
using ClinicSystem.Infrastructure.Persistence.Repositories;

namespace ClinicSystem.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ClinicDbContext _context;
    private readonly IServiceProvider _serviceProvider;

    public UnitOfWork(
        ClinicDbContext context,
        IPatientRepository patients,
        IMedicationRepository medications,
        IServiceProvider serviceProvider)
    {
        _context = context;
        Patients = patients;
        Medications = medications;
        _serviceProvider = serviceProvider;
    }

    public IPatientRepository Patients { get; }
    
    public IMedicationRepository Medications { get; }

    public IRepository<T> Repository<T>() where T : class
    {
        return (IRepository<T>)_serviceProvider.GetService(typeof(IRepository<T>))!;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
