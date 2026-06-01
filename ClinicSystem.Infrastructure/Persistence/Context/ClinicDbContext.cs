using System;
using System.Collections.Generic;
using ClinicSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicSystem.Infrastructure.Persistence.Context;

public partial class ClinicDbContext : DbContext
{
    public ClinicDbContext(DbContextOptions<ClinicDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<AppointmentStatus> AppointmentStatuses { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Billing> Billings { get; set; }

    public virtual DbSet<BillingDetail> BillingDetails { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Diagnosis> Diagnoses { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<InsuranceCompany> InsuranceCompanies { get; set; }

    public virtual DbSet<InsurancePolicy> InsurancePolicies { get; set; }

    public virtual DbSet<LaboratoryResult> LaboratoryResults { get; set; }

    public virtual DbSet<LaboratoryTest> LaboratoryTests { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<Medication> Medications { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationType> NotificationTypes { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<PrescriptionDetail> PrescriptionDetails { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Specialty> Specialties { get; set; }

    public virtual DbSet<StockMovement> StockMovements { get; set; }

    public virtual DbSet<Treatment> Treatments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VitalSign> VitalSigns { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pgcrypto");

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("appointments_pkey");

            entity.ToTable("appointments", "clinical");

            entity.HasIndex(e => e.AppointmentDate, "idx_appointments_date");

            entity.HasIndex(e => e.DoctorId, "idx_appointments_doctor");

            entity.HasIndex(e => e.PatientId, "idx_appointments_patient");

            entity.HasIndex(e => new { e.DoctorId, e.AppointmentDate }, "uix_appointment_slot")
                .IsUnique()
                .HasFilter("(deleted_at IS NULL)");

            entity.Property(e => e.AppointmentId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("appointment_id");
            entity.Property(e => e.AppointmentDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("appointment_date");
            entity.Property(e => e.CancellationReason).HasColumnName("cancellation_reason");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.DurationMinutes)
                .HasDefaultValue(30)
                .HasColumnName("duration_minutes");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.RescheduledFrom).HasColumnName("rescheduled_from");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.AppointmentCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("appointments_created_by_fkey");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("appointments_doctor_id_fkey");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("appointments_patient_id_fkey");

            entity.HasOne(d => d.RescheduledFromNavigation).WithMany(p => p.InverseRescheduledFromNavigation)
                .HasForeignKey(d => d.RescheduledFrom)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("appointments_rescheduled_from_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("appointments_status_id_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.AppointmentUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("appointments_updated_by_fkey");
        });

        modelBuilder.Entity<AppointmentStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("appointment_statuses_pkey");

            entity.ToTable("appointment_statuses", "clinical");

            entity.HasIndex(e => e.Name, "appointment_statuses_name_key").IsUnique();

            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("status_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditLogId).HasName("audit_logs_pkey");

            entity.ToTable("audit_logs", "auth");

            entity.HasIndex(e => e.CreatedAt, "idx_audit_created");

            entity.HasIndex(e => new { e.EntityName, e.EntityId }, "idx_audit_entity");

            entity.HasIndex(e => e.UserId, "idx_audit_user");

            entity.Property(e => e.AuditLogId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("audit_log_id");
            entity.Property(e => e.Action)
                .HasMaxLength(100)
                .HasColumnName("action");
            entity.Property(e => e.CorrelationId).HasColumnName("correlation_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.EntityId).HasColumnName("entity_id");
            entity.Property(e => e.EntityName)
                .HasMaxLength(100)
                .HasColumnName("entity_name");
            entity.Property(e => e.IpAddress).HasColumnName("ip_address");
            entity.Property(e => e.NewValues)
                .HasColumnType("jsonb")
                .HasColumnName("new_values");
            entity.Property(e => e.OldValues)
                .HasColumnType("jsonb")
                .HasColumnName("old_values");
            entity.Property(e => e.UserAgent).HasColumnName("user_agent");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("audit_logs_user_id_fkey");
        });

        modelBuilder.Entity<Billing>(entity =>
        {
            entity.HasKey(e => e.BillingId).HasName("billing_pkey");

            entity.ToTable("billing", "billing");

            entity.HasIndex(e => e.IssueDate, "idx_billing_issue_date");

            entity.HasIndex(e => e.PatientId, "idx_billing_patient");

            entity.HasIndex(e => e.Status, "idx_billing_status");

            entity.Property(e => e.BillingId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("billing_id");
            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Discount)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("discount");
            entity.Property(e => e.InsuranceCoverage)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("insurance_coverage");
            entity.Property(e => e.InsurancePolicyId).HasColumnName("insurance_policy_id");
            entity.Property(e => e.IssueDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("issue_date");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .HasColumnName("status");
            entity.Property(e => e.Subtotal)
                .HasPrecision(12, 2)
                .HasColumnName("subtotal");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(12, 2)
                .HasComputedColumnSql("((subtotal - discount) - insurance_coverage)", true)
                .HasColumnName("total_amount");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Appointment).WithMany(p => p.Billings)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("billing_appointment_id_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.BillingCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("billing_created_by_fkey");

            entity.HasOne(d => d.InsurancePolicy).WithMany(p => p.Billings)
                .HasForeignKey(d => d.InsurancePolicyId)
                .HasConstraintName("billing_insurance_policy_id_fkey");

            entity.HasOne(d => d.Patient).WithMany(p => p.Billings)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("billing_patient_id_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.BillingUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("billing_updated_by_fkey");
        });

        modelBuilder.Entity<BillingDetail>(entity =>
        {
            entity.HasKey(e => e.BillingDetailId).HasName("billing_details_pkey");

            entity.ToTable("billing_details", "billing");

            entity.Property(e => e.BillingDetailId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("billing_detail_id");
            entity.Property(e => e.Amount)
                .HasPrecision(12, 2)
                .HasComputedColumnSql("((quantity)::numeric * unit_price)", true)
                .HasColumnName("amount");
            entity.Property(e => e.BillingId).HasColumnName("billing_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(12, 2)
                .HasColumnName("unit_price");

            entity.HasOne(d => d.Billing).WithMany(p => p.BillingDetails)
                .HasForeignKey(d => d.BillingId)
                .HasConstraintName("billing_details_billing_id_fkey");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("departments_pkey");

            entity.ToTable("departments", "clinical");

            entity.HasIndex(e => e.Name, "departments_name_key").IsUnique();

            entity.Property(e => e.DepartmentId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("department_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Diagnosis>(entity =>
        {
            entity.HasKey(e => e.DiagnosisId).HasName("diagnoses_pkey");

            entity.ToTable("diagnoses", "clinical");

            entity.Property(e => e.DiagnosisId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("diagnosis_id");
            entity.Property(e => e.Cie10Code)
                .HasMaxLength(10)
                .HasColumnName("cie10_code");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsPrimary)
                .HasDefaultValue(false)
                .HasColumnName("is_primary");
            entity.Property(e => e.MedicalRecordId).HasColumnName("medical_record_id");
            entity.Property(e => e.NotedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("noted_at");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.MedicalRecordId)
                .HasConstraintName("diagnoses_medical_record_id_fkey");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("doctors_pkey");

            entity.ToTable("doctors", "clinical");

            entity.HasIndex(e => e.LicenseNumber, "doctors_license_number_key").IsUnique();

            entity.HasIndex(e => e.UserId, "doctors_user_id_key").IsUnique();

            entity.Property(e => e.DoctorId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("doctor_id");
            entity.Property(e => e.ConsultationFee)
                .HasPrecision(10, 2)
                .HasColumnName("consultation_fee");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LicenseNumber)
                .HasMaxLength(50)
                .HasColumnName("license_number");
            entity.Property(e => e.Office)
                .HasMaxLength(100)
                .HasColumnName("office");
            entity.Property(e => e.SpecialtyId).HasColumnName("specialty_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.YearsExperience).HasColumnName("years_experience");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DoctorCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("doctors_created_by_fkey");

            entity.HasOne(d => d.Specialty).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.SpecialtyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doctors_specialty_id_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.DoctorUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("doctors_updated_by_fkey");

            entity.HasOne(d => d.User).WithOne(p => p.DoctorUser)
                .HasForeignKey<Doctor>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("doctors_user_id_fkey");
        });

        modelBuilder.Entity<InsuranceCompany>(entity =>
        {
            entity.HasKey(e => e.InsuranceCompanyId).HasName("insurance_companies_pkey");

            entity.ToTable("insurance_companies", "billing");

            entity.HasIndex(e => e.Name, "insurance_companies_name_key").IsUnique();

            entity.Property(e => e.InsuranceCompanyId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("insurance_company_id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.ContactName)
                .HasMaxLength(100)
                .HasColumnName("contact_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<InsurancePolicy>(entity =>
        {
            entity.HasKey(e => e.InsurancePolicyId).HasName("insurance_policies_pkey");

            entity.ToTable("insurance_policies", "billing");

            entity.HasIndex(e => e.PolicyNumber, "insurance_policies_policy_number_key").IsUnique();

            entity.Property(e => e.InsurancePolicyId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("insurance_policy_id");
            entity.Property(e => e.CoveragePercentage)
                .HasPrecision(5, 2)
                .HasColumnName("coverage_percentage");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.InsuranceCompanyId).HasColumnName("insurance_company_id");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.MaxCoverageAmount)
                .HasPrecision(12, 2)
                .HasColumnName("max_coverage_amount");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.PolicyNumber)
                .HasMaxLength(100)
                .HasColumnName("policy_number");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InsurancePolicyCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("insurance_policies_created_by_fkey");

            entity.HasOne(d => d.InsuranceCompany).WithMany(p => p.InsurancePolicies)
                .HasForeignKey(d => d.InsuranceCompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("insurance_policies_insurance_company_id_fkey");

            entity.HasOne(d => d.Patient).WithMany(p => p.InsurancePolicies)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("insurance_policies_patient_id_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.InsurancePolicyUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("insurance_policies_updated_by_fkey");
        });

        modelBuilder.Entity<LaboratoryResult>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("laboratory_results_pkey");

            entity.ToTable("laboratory_results", "laboratory");

            entity.Property(e => e.ResultId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("result_id");
            entity.Property(e => e.IsAbnormal)
                .HasDefaultValue(false)
                .HasColumnName("is_abnormal");
            entity.Property(e => e.LaboratoryTestId).HasColumnName("laboratory_test_id");
            entity.Property(e => e.NotedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("noted_at");
            entity.Property(e => e.ParameterName)
                .HasMaxLength(100)
                .HasColumnName("parameter_name");
            entity.Property(e => e.ReferenceRange)
                .HasMaxLength(100)
                .HasColumnName("reference_range");
            entity.Property(e => e.ResultValue).HasColumnName("result_value");
            entity.Property(e => e.Unit)
                .HasMaxLength(50)
                .HasColumnName("unit");

            entity.HasOne(d => d.LaboratoryTest).WithMany(p => p.LaboratoryResults)
                .HasForeignKey(d => d.LaboratoryTestId)
                .HasConstraintName("laboratory_results_laboratory_test_id_fkey");
        });

        modelBuilder.Entity<LaboratoryTest>(entity =>
        {
            entity.HasKey(e => e.LaboratoryTestId).HasName("laboratory_tests_pkey");

            entity.ToTable("laboratory_tests", "laboratory");

            entity.HasIndex(e => e.DoctorId, "idx_lab_tests_doctor");

            entity.HasIndex(e => e.PatientId, "idx_lab_tests_patient");

            entity.HasIndex(e => e.Status, "idx_lab_tests_status");

            entity.Property(e => e.LaboratoryTestId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("laboratory_test_id");
            entity.Property(e => e.CompletedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("completed_date");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.MedicalRecordId).HasColumnName("medical_record_id");
            entity.Property(e => e.Observations).HasColumnName("observations");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.RequestedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("requested_date");
            entity.Property(e => e.SampleTakenDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("sample_taken_date");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .HasColumnName("status");
            entity.Property(e => e.TestName)
                .HasMaxLength(150)
                .HasColumnName("test_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.LaboratoryTestCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("laboratory_tests_created_by_fkey");

            entity.HasOne(d => d.Doctor).WithMany(p => p.LaboratoryTests)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("laboratory_tests_doctor_id_fkey");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.LaboratoryTests)
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("laboratory_tests_medical_record_id_fkey");

            entity.HasOne(d => d.Patient).WithMany(p => p.LaboratoryTests)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("laboratory_tests_patient_id_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.LaboratoryTestUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("laboratory_tests_updated_by_fkey");
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.MedicalRecordId).HasName("medical_records_pkey");

            entity.ToTable("medical_records", "clinical");

            entity.HasIndex(e => e.DoctorId, "idx_medical_records_doctor");

            entity.HasIndex(e => e.PatientId, "idx_medical_records_patient");

            entity.Property(e => e.MedicalRecordId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("medical_record_id");
            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.ChiefComplaint).HasColumnName("chief_complaint");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Diagnosis).HasColumnName("diagnosis");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.Observations).HasColumnName("observations");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Treatment).HasColumnName("treatment");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Appointment).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("medical_records_appointment_id_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MedicalRecordCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("medical_records_created_by_fkey");

            entity.HasOne(d => d.Doctor).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("medical_records_doctor_id_fkey");

            entity.HasOne(d => d.Patient).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("medical_records_patient_id_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.MedicalRecordUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("medical_records_updated_by_fkey");
        });

        modelBuilder.Entity<Medication>(entity =>
        {
            entity.HasKey(e => e.MedicationId).HasName("medications_pkey");

            entity.ToTable("medications", "pharmacy");

            entity.Property(e => e.MedicationId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("medication_id");
            entity.Property(e => e.Concentration)
                .HasMaxLength(50)
                .HasColumnName("concentration");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.GenericName)
                .HasMaxLength(150)
                .HasColumnName("generic_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Laboratory)
                .HasMaxLength(100)
                .HasColumnName("laboratory");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.Presentation)
                .HasMaxLength(100)
                .HasColumnName("presentation");
            entity.Property(e => e.RequiresPrescription)
                .HasDefaultValue(true)
                .HasColumnName("requires_prescription");
            entity.Property(e => e.Stock)
                .HasDefaultValue(0)
                .HasColumnName("stock");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(10, 2)
                .HasColumnName("unit_price");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MedicationCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("medications_created_by_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.MedicationUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("medications_updated_by_fkey");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("notifications_pkey");

            entity.ToTable("notifications", "notifications");

            entity.HasIndex(e => e.Status, "idx_notifications_status");

            entity.HasIndex(e => e.UserId, "idx_notifications_user");

            entity.Property(e => e.NotificationId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("notification_id");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.Channel)
                .HasMaxLength(20)
                .HasColumnName("channel");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.EntityId).HasColumnName("entity_id");
            entity.Property(e => e.EntityType)
                .HasMaxLength(50)
                .HasColumnName("entity_type");
            entity.Property(e => e.ReadAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("read_at");
            entity.Property(e => e.ScheduledAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("scheduled_at");
            entity.Property(e => e.SentAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("sent_at");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pending'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.Subject).HasColumnName("subject");
            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Type).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notifications_type_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("notifications_user_id_fkey");
        });

        modelBuilder.Entity<NotificationType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("notification_types_pkey");

            entity.ToTable("notification_types", "notifications");

            entity.HasIndex(e => e.Code, "notification_types_code_key").IsUnique();

            entity.Property(e => e.TypeId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("type_id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.TemplateBody).HasColumnName("template_body");
            entity.Property(e => e.TemplateSubject).HasColumnName("template_subject");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("patients_pkey");

            entity.ToTable("patients", "clinical");

            entity.HasIndex(e => e.DocumentNumber, "patients_document_number_key").IsUnique();

            entity.HasIndex(e => e.UserId, "patients_user_id_key").IsUnique();

            entity.Property(e => e.PatientId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("patient_id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.BloodType)
                .HasMaxLength(5)
                .HasColumnName("blood_type");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DocumentNumber)
                .HasMaxLength(20)
                .HasColumnName("document_number");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.EmergencyContactName)
                .HasMaxLength(100)
                .HasColumnName("emergency_contact_name");
            entity.Property(e => e.EmergencyContactPhone)
                .HasMaxLength(20)
                .HasColumnName("emergency_contact_phone");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .HasColumnName("gender");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PatientCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("patients_created_by_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.PatientUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("patients_updated_by_fkey");

            entity.HasOne(d => d.User).WithOne(p => p.PatientUser)
                .HasForeignKey<Patient>(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("patients_user_id_fkey");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("payments_pkey");

            entity.ToTable("payments", "billing");

            entity.HasIndex(e => e.BillingId, "idx_payments_billing");

            entity.Property(e => e.PaymentId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasPrecision(12, 2)
                .HasColumnName("amount");
            entity.Property(e => e.BillingId).HasColumnName("billing_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.InsurancePolicyId).HasColumnName("insurance_policy_id");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("payment_method");
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(100)
                .HasColumnName("reference_number");
            entity.Property(e => e.RegisteredBy).HasColumnName("registered_by");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .HasDefaultValueSql("'pending'::character varying")
                .HasColumnName("status");

            entity.HasOne(d => d.Billing).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BillingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payments_billing_id_fkey");

            entity.HasOne(d => d.InsurancePolicy).WithMany(p => p.Payments)
                .HasForeignKey(d => d.InsurancePolicyId)
                .HasConstraintName("payments_insurance_policy_id_fkey");

            entity.HasOne(d => d.RegisteredByNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.RegisteredBy)
                .HasConstraintName("payments_registered_by_fkey");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("permissions_pkey");

            entity.ToTable("permissions", "auth");

            entity.HasIndex(e => new { e.Resource, e.Action }, "permissions_resource_action_key").IsUnique();

            entity.Property(e => e.PermissionId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("permission_id");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .HasColumnName("action");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Resource)
                .HasMaxLength(100)
                .HasColumnName("resource");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.PrescriptionId).HasName("prescriptions_pkey");

            entity.ToTable("prescriptions", "pharmacy");

            entity.HasIndex(e => e.MedicalRecordId, "idx_prescriptions_medical_record");

            entity.HasIndex(e => e.PatientId, "idx_prescriptions_patient");

            entity.Property(e => e.PrescriptionId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("prescription_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DispensedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dispensed_at");
            entity.Property(e => e.DispensedBy).HasColumnName("dispensed_by");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.MedicalRecordId).HasColumnName("medical_record_id");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.ValidUntil).HasColumnName("valid_until");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PrescriptionCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("prescriptions_created_by_fkey");

            entity.HasOne(d => d.DispensedByNavigation).WithMany(p => p.PrescriptionDispensedByNavigations)
                .HasForeignKey(d => d.DispensedBy)
                .HasConstraintName("prescriptions_dispensed_by_fkey");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prescriptions_doctor_id_fkey");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.MedicalRecordId)
                .HasConstraintName("prescriptions_medical_record_id_fkey");

            entity.HasOne(d => d.Patient).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prescriptions_patient_id_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.PrescriptionUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("prescriptions_updated_by_fkey");
        });

        modelBuilder.Entity<PrescriptionDetail>(entity =>
        {
            entity.HasKey(e => e.PrescriptionDetailId).HasName("prescription_details_pkey");

            entity.ToTable("prescription_details", "pharmacy");

            entity.Property(e => e.PrescriptionDetailId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("prescription_detail_id");
            entity.Property(e => e.Dosage)
                .HasMaxLength(100)
                .HasColumnName("dosage");
            entity.Property(e => e.DurationDays).HasColumnName("duration_days");
            entity.Property(e => e.Frequency)
                .HasMaxLength(100)
                .HasColumnName("frequency");
            entity.Property(e => e.Instructions).HasColumnName("instructions");
            entity.Property(e => e.IsSubstitutable)
                .HasDefaultValue(true)
                .HasColumnName("is_substitutable");
            entity.Property(e => e.MedicationId).HasColumnName("medication_id");
            entity.Property(e => e.PrescriptionId).HasColumnName("prescription_id");
            entity.Property(e => e.QuantityPrescribed).HasColumnName("quantity_prescribed");

            entity.HasOne(d => d.Medication).WithMany(p => p.PrescriptionDetails)
                .HasForeignKey(d => d.MedicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prescription_details_medication_id_fkey");

            entity.HasOne(d => d.Prescription).WithMany(p => p.PrescriptionDetails)
                .HasForeignKey(d => d.PrescriptionId)
                .HasConstraintName("prescription_details_prescription_id_fkey");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.RefreshTokenId).HasName("refresh_tokens_pkey");

            entity.ToTable("refresh_tokens", "auth");

            entity.Property(e => e.RefreshTokenId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("refresh_token_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expires_at");
            entity.Property(e => e.RevokedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("revoked_at");
            entity.Property(e => e.TokenHash).HasColumnName("token_hash");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("refresh_tokens_user_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("roles_pkey");

            entity.ToTable("roles", "auth");

            entity.HasIndex(e => e.Name, "roles_name_key").IsUnique();

            entity.Property(e => e.RoleId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("role_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .HasConstraintName("role_permissions_permission_id_fkey"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("role_permissions_role_id_fkey"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId").HasName("role_permissions_pkey");
                        j.ToTable("role_permissions", "auth");
                        j.IndexerProperty<Guid>("RoleId").HasColumnName("role_id");
                        j.IndexerProperty<Guid>("PermissionId").HasColumnName("permission_id");
                    });
        });

        modelBuilder.Entity<Specialty>(entity =>
        {
            entity.HasKey(e => e.SpecialtyId).HasName("specialties_pkey");

            entity.ToTable("specialties", "clinical");

            entity.HasIndex(e => e.Name, "specialties_name_key").IsUnique();

            entity.Property(e => e.SpecialtyId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("specialty_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Department).WithMany(p => p.Specialties)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("specialties_department_id_fkey");
        });

        modelBuilder.Entity<StockMovement>(entity =>
        {
            entity.HasKey(e => e.MovementId).HasName("stock_movements_pkey");

            entity.ToTable("stock_movements", "pharmacy");

            entity.HasIndex(e => e.MedicationId, "idx_stock_movements_medication");

            entity.Property(e => e.MovementId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("movement_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.MedicationId).HasColumnName("medication_id");
            entity.Property(e => e.MovementType)
                .HasMaxLength(30)
                .HasColumnName("movement_type");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.PerformedBy).HasColumnName("performed_by");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ReferenceId).HasColumnName("reference_id");

            entity.HasOne(d => d.Medication).WithMany(p => p.StockMovements)
                .HasForeignKey(d => d.MedicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stock_movements_medication_id_fkey");

            entity.HasOne(d => d.PerformedByNavigation).WithMany(p => p.StockMovements)
                .HasForeignKey(d => d.PerformedBy)
                .HasConstraintName("stock_movements_performed_by_fkey");
        });

        modelBuilder.Entity<Treatment>(entity =>
        {
            entity.HasKey(e => e.TreatmentId).HasName("treatments_pkey");

            entity.ToTable("treatments", "clinical");

            entity.Property(e => e.TreatmentId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("treatment_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.MedicalRecordId).HasColumnName("medical_record_id");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .HasDefaultValueSql("'active'::character varying")
                .HasColumnName("status");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.Treatments)
                .HasForeignKey(d => d.MedicalRecordId)
                .HasConstraintName("treatments_medical_record_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users", "auth");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("users_created_by_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_id_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.InverseUpdatedByNavigation)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("users_updated_by_fkey");
        });

        modelBuilder.Entity<VitalSign>(entity =>
        {
            entity.HasKey(e => e.VitalSignId).HasName("vital_signs_pkey");

            entity.ToTable("vital_signs", "clinical");

            entity.Property(e => e.VitalSignId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("vital_sign_id");
            entity.Property(e => e.DiastolicBp).HasColumnName("diastolic_bp");
            entity.Property(e => e.HeartRate).HasColumnName("heart_rate");
            entity.Property(e => e.HeightCm)
                .HasPrecision(5, 2)
                .HasColumnName("height_cm");
            entity.Property(e => e.MedicalRecordId).HasColumnName("medical_record_id");
            entity.Property(e => e.RecordedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("recorded_at");
            entity.Property(e => e.RecordedBy).HasColumnName("recorded_by");
            entity.Property(e => e.RespiratoryRate).HasColumnName("respiratory_rate");
            entity.Property(e => e.Spo2).HasColumnName("spo2");
            entity.Property(e => e.SystolicBp).HasColumnName("systolic_bp");
            entity.Property(e => e.Temperature)
                .HasPrecision(4, 1)
                .HasColumnName("temperature");
            entity.Property(e => e.WeightKg)
                .HasPrecision(5, 2)
                .HasColumnName("weight_kg");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.VitalSigns)
                .HasForeignKey(d => d.MedicalRecordId)
                .HasConstraintName("vital_signs_medical_record_id_fkey");

            entity.HasOne(d => d.RecordedByNavigation).WithMany(p => p.VitalSigns)
                .HasForeignKey(d => d.RecordedBy)
                .HasConstraintName("vital_signs_recorded_by_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
