using ClinicSystem.Domain.Ports.Persistence;
using ClinicSystem.Infrastructure.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicSystem.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ClinicDbContext _context;
    private readonly IServiceProvider _serviceProvider;

    public UnitOfWork(
        ClinicDbContext context,
        IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }

    public IAppointmentRepository Appointments => _serviceProvider.GetRequiredService<IAppointmentRepository>();
    public IAppointmentStatusRepository AppointmentStatuses => _serviceProvider.GetRequiredService<IAppointmentStatusRepository>();
    public IAuditLogRepository AuditLogs => _serviceProvider.GetRequiredService<IAuditLogRepository>();
    public IBillingRepository Billings => _serviceProvider.GetRequiredService<IBillingRepository>();
    public IBillingDetailRepository BillingDetails => _serviceProvider.GetRequiredService<IBillingDetailRepository>();
    public IDepartmentRepository Departments => _serviceProvider.GetRequiredService<IDepartmentRepository>();
    public IDiagnosisRepository Diagnoses => _serviceProvider.GetRequiredService<IDiagnosisRepository>();
    public IDoctorRepository Doctors => _serviceProvider.GetRequiredService<IDoctorRepository>();
    public IInsuranceCompanyRepository InsuranceCompanies => _serviceProvider.GetRequiredService<IInsuranceCompanyRepository>();
    public IInsurancePolicyRepository InsurancePolicies => _serviceProvider.GetRequiredService<IInsurancePolicyRepository>();
    public ILaboratoryResultRepository LaboratoryResults => _serviceProvider.GetRequiredService<ILaboratoryResultRepository>();
    public ILaboratoryTestRepository LaboratoryTests => _serviceProvider.GetRequiredService<ILaboratoryTestRepository>();
    public IMedicalRecordRepository MedicalRecords => _serviceProvider.GetRequiredService<IMedicalRecordRepository>();
    public IMedicationRepository Medications => _serviceProvider.GetRequiredService<IMedicationRepository>();
    public INotificationRepository Notifications => _serviceProvider.GetRequiredService<INotificationRepository>();
    public INotificationTypeRepository NotificationTypes => _serviceProvider.GetRequiredService<INotificationTypeRepository>();
    public IPatientRepository Patients => _serviceProvider.GetRequiredService<IPatientRepository>();
    public IPaymentRepository Payments => _serviceProvider.GetRequiredService<IPaymentRepository>();
    public IPermissionRepository Permissions => _serviceProvider.GetRequiredService<IPermissionRepository>();
    public IPrescriptionRepository Prescriptions => _serviceProvider.GetRequiredService<IPrescriptionRepository>();
    public IPrescriptionDetailRepository PrescriptionDetails => _serviceProvider.GetRequiredService<IPrescriptionDetailRepository>();
    public IRefreshTokenRepository RefreshTokens => _serviceProvider.GetRequiredService<IRefreshTokenRepository>();
    public IRoleRepository Roles => _serviceProvider.GetRequiredService<IRoleRepository>();
    public ISpecialtyRepository Specialties => _serviceProvider.GetRequiredService<ISpecialtyRepository>();
    public IStockMovementRepository StockMovements => _serviceProvider.GetRequiredService<IStockMovementRepository>();
    public ITreatmentRepository Treatments => _serviceProvider.GetRequiredService<ITreatmentRepository>();
    public IUserRepository Users => _serviceProvider.GetRequiredService<IUserRepository>();
    public IVitalSignRepository VitalSigns => _serviceProvider.GetRequiredService<IVitalSignRepository>();

    public IRepository<T> Repository<T>() where T : class
    {
        return _serviceProvider.GetRequiredService<IRepository<T>>();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
