namespace ClinicSystem.Domain.Ports.Persistence;

public interface IUnitOfWork
{
    IAppointmentRepository Appointments { get; }
    IAppointmentStatusRepository AppointmentStatuses { get; }
    IAuditLogRepository AuditLogs { get; }
    IBillingRepository Billings { get; }
    IBillingDetailRepository BillingDetails { get; }
    IDepartmentRepository Departments { get; }
    IDiagnosisRepository Diagnoses { get; }
    IDoctorRepository Doctors { get; }
    IInsuranceCompanyRepository InsuranceCompanies { get; }
    IInsurancePolicyRepository InsurancePolicies { get; }
    ILaboratoryResultRepository LaboratoryResults { get; }
    ILaboratoryTestRepository LaboratoryTests { get; }
    IMedicalRecordRepository MedicalRecords { get; }
    IMedicationRepository Medications { get; }
    INotificationRepository Notifications { get; }
    INotificationTypeRepository NotificationTypes { get; }
    IPatientRepository Patients { get; }
    IPaymentRepository Payments { get; }
    IPermissionRepository Permissions { get; }
    IPrescriptionRepository Prescriptions { get; }
    IPrescriptionDetailRepository PrescriptionDetails { get; }
    IRefreshTokenRepository RefreshTokens { get; }
    IRoleRepository Roles { get; }
    ISpecialtyRepository Specialties { get; }
    IStockMovementRepository StockMovements { get; }
    ITreatmentRepository Treatments { get; }
    IUserRepository Users { get; }
    IVitalSignRepository VitalSigns { get; }

    IRepository<T> Repository<T>() where T : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
