using ClinicSystem.Domain.Ports.Repositories;
using ClinicSystem.Infrastructure.Persistence.Context;

namespace ClinicSystem.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ClinicDbContext _context;
    private readonly IServiceProvider _serviceProvider;

    public UnitOfWork(
        ClinicDbContext context,
        IPatientRepository patients,
        IMedicationRepository medications,
        IDepartmentRepository departments,
        INotificationTypeRepository notificationTypes,
        IServiceProvider serviceProvider)
    {
        _context = context;
        Patients = patients;
        Medications = medications;
        Departments = departments;
        NotificationTypes = notificationTypes;
        _serviceProvider = serviceProvider;
    }

    public IPatientRepository Patients { get; }
    
    public IMedicationRepository Medications { get; }
    
    public IDepartmentRepository Departments { get; }

    public INotificationTypeRepository NotificationTypes { get; }

    public IRepository<T> Repository<T>() where T : class
    {
        return (IRepository<T>)_serviceProvider.GetService(typeof(IRepository<T>))!;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
