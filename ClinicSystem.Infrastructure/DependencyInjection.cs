using ClinicSystem.Domain.Ports.Persistence;
using ClinicSystem.Infrastructure.Persistence;
using ClinicSystem.Infrastructure.Persistence.Context;
using ClinicSystem.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ClinicDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IAppointmentStatusRepository, AppointmentStatusRepository>();
        services.AddScoped<IAuditLogRepository, AuditLogRepository>();
        services.AddScoped<IBillingRepository, BillingRepository>();
        services.AddScoped<IBillingDetailRepository, BillingDetailRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IDiagnosisRepository, DiagnosisRepository>();
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IInsuranceCompanyRepository, InsuranceCompanyRepository>();
        services.AddScoped<IInsurancePolicyRepository, InsurancePolicyRepository>();
        services.AddScoped<ILaboratoryResultRepository, LaboratoryResultRepository>();
        services.AddScoped<ILaboratoryTestRepository, LaboratoryTestRepository>();
        services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
        services.AddScoped<IMedicationRepository, MedicationRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<INotificationTypeRepository, NotificationTypeRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
        services.AddScoped<IPrescriptionDetailRepository, PrescriptionDetailRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
        services.AddScoped<IStockMovementRepository, StockMovementRepository>();
        services.AddScoped<ITreatmentRepository, TreatmentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVitalSignRepository, VitalSignRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
