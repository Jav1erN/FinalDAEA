namespace ClinicSystem.Application.Ports.Persistence;

public interface IUnitOfWork
{
    IPatientRepository Patients { get; }
    
    IMedicationRepository Medications { get; }
    
    IDepartmentRepository Departments { get; }
    
    INotificationTypeRepository NotificationTypes { get; }

    IRepository<T> Repository<T>() where T : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
