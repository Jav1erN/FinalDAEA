namespace ClinicSystem.Domain.Ports.Persistence;

public interface IUnitOfWork
{
    IPatientRepository Patients { get; }

    IRepository<T> Repository<T>() where T : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
