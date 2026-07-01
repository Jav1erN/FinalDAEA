using ClinicSystem.Domain.Ports.Persistence;
using ClinicSystem.Domain.Ports.Services;
using ClinicSystem.Infrastructure.Adapters.Services;
using ClinicSystem.Infrastructure.Persistence;
using ClinicSystem.Infrastructure.Persistence.Context;
using ClinicSystem.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace ClinicSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ClinicDbContext>(options =>
        {
            options.UseNpgsql(GetPostgresConnectionString(configuration));
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

        services.AddScoped<IAppointmentSchedulingService, AppointmentSchedulingService>();
        services.AddScoped<IBillingPolicyService, BillingPolicyService>();
        services.AddScoped<IInventoryPolicyService, InventoryPolicyService>();

        return services;
    }

    private static string GetPostgresConnectionString(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (!string.IsNullOrWhiteSpace(connectionString))
        {
            return connectionString;
        }

        var databaseUrl = configuration["DATABASE_URL"];
        if (!string.IsNullOrWhiteSpace(databaseUrl))
        {
            return ConvertDatabaseUrl(databaseUrl);
        }

        throw new InvalidOperationException(
            "Database connection string not configured. Set ConnectionStrings:DefaultConnection or DATABASE_URL.");
    }

    private static string ConvertDatabaseUrl(string databaseUrl)
    {
        var uri = new Uri(databaseUrl);
        var userInfo = uri.UserInfo.Split(':', 2);

        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = uri.Host,
            Port = uri.Port > 0 ? uri.Port : 5432,
            Database = uri.AbsolutePath.TrimStart('/'),
            Username = Uri.UnescapeDataString(userInfo[0]),
            Password = userInfo.Length > 1 ? Uri.UnescapeDataString(userInfo[1]) : string.Empty
        };

        var queryParameters = System.Web.HttpUtility.ParseQueryString(uri.Query);
        var sslMode = queryParameters["sslmode"];
        if (!string.IsNullOrWhiteSpace(sslMode) &&
            Enum.TryParse<SslMode>(sslMode, true, out var parsedSslMode))
        {
            builder.SslMode = parsedSslMode;
        }

        return builder.ConnectionString;
    }
}
