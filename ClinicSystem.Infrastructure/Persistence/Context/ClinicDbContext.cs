using System;
using System.Collections.Generic;
using ClinicSystem.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicSystem.Infrastructure.Persistence.Context;

public partial class ClinicDbContext : DbContext
{
    public ClinicDbContext(DbContextOptions<ClinicDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<appointment> appointments { get; set; }

    public virtual DbSet<appointment_status> appointment_statuses { get; set; }

    public virtual DbSet<audit_log> audit_logs { get; set; }

    public virtual DbSet<billing> billings { get; set; }

    public virtual DbSet<billing_detail> billing_details { get; set; }

    public virtual DbSet<department> departments { get; set; }

    public virtual DbSet<diagnosis> diagnoses { get; set; }

    public virtual DbSet<doctor> doctors { get; set; }

    public virtual DbSet<insurance_company> insurance_companies { get; set; }

    public virtual DbSet<insurance_policy> insurance_policies { get; set; }

    public virtual DbSet<laboratory_result> laboratory_results { get; set; }

    public virtual DbSet<laboratory_test> laboratory_tests { get; set; }

    public virtual DbSet<medical_record> medical_records { get; set; }

    public virtual DbSet<medication> medications { get; set; }

    public virtual DbSet<notification> notifications { get; set; }

    public virtual DbSet<notification_type> notification_types { get; set; }

    public virtual DbSet<patient> patients { get; set; }

    public virtual DbSet<payment> payments { get; set; }

    public virtual DbSet<permission> permissions { get; set; }

    public virtual DbSet<prescription> prescriptions { get; set; }

    public virtual DbSet<prescription_detail> prescription_details { get; set; }

    public virtual DbSet<refresh_token> refresh_tokens { get; set; }

    public virtual DbSet<role> roles { get; set; }

    public virtual DbSet<specialty> specialties { get; set; }

    public virtual DbSet<stock_movement> stock_movements { get; set; }

    public virtual DbSet<treatment> treatments { get; set; }

    public virtual DbSet<user> users { get; set; }

    public virtual DbSet<vital_sign> vital_signs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pgcrypto");

        modelBuilder.Entity<appointment>(entity =>
        {
            entity.HasKey(e => e.appointment_id).HasName("appointments_pkey");

            entity.ToTable("appointments", "clinical");

            entity.HasIndex(e => e.appointment_date, "idx_appointments_date");

            entity.HasIndex(e => e.doctor_id, "idx_appointments_doctor");

            entity.HasIndex(e => e.patient_id, "idx_appointments_patient");

            entity.HasIndex(e => new { e.doctor_id, e.appointment_date }, "uix_appointment_slot")
                .IsUnique()
                .HasFilter("(deleted_at IS NULL)");

            entity.Property(e => e.appointment_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.appointment_date).HasColumnType("timestamp without time zone");
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.deleted_at).HasColumnType("timestamp without time zone");
            entity.Property(e => e.duration_minutes).HasDefaultValue(30);
            entity.Property(e => e.updated_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.created_byNavigation).WithMany(p => p.appointmentcreated_byNavigations)
                .HasForeignKey(d => d.created_by)
                .HasConstraintName("appointments_created_by_fkey");

            entity.HasOne(d => d.doctor).WithMany(p => p.appointments)
                .HasForeignKey(d => d.doctor_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("appointments_doctor_id_fkey");

            entity.HasOne(d => d.patient).WithMany(p => p.appointments)
                .HasForeignKey(d => d.patient_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("appointments_patient_id_fkey");

            entity.HasOne(d => d.rescheduled_fromNavigation).WithMany(p => p.Inverserescheduled_fromNavigation)
                .HasForeignKey(d => d.rescheduled_from)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("appointments_rescheduled_from_fkey");

            entity.HasOne(d => d.status).WithMany(p => p.appointments)
                .HasForeignKey(d => d.status_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("appointments_status_id_fkey");

            entity.HasOne(d => d.updated_byNavigation).WithMany(p => p.appointmentupdated_byNavigations)
                .HasForeignKey(d => d.updated_by)
                .HasConstraintName("appointments_updated_by_fkey");
        });

        modelBuilder.Entity<appointment_status>(entity =>
        {
            entity.HasKey(e => e.status_id).HasName("appointment_statuses_pkey");

            entity.ToTable("appointment_statuses", "clinical");

            entity.HasIndex(e => e.name, "appointment_statuses_name_key").IsUnique();

            entity.Property(e => e.status_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.name).HasMaxLength(30);
        });

        modelBuilder.Entity<audit_log>(entity =>
        {
            entity.HasKey(e => e.audit_log_id).HasName("audit_logs_pkey");

            entity.ToTable("audit_logs", "auth");

            entity.HasIndex(e => e.created_at, "idx_audit_created");

            entity.HasIndex(e => new { e.entity_name, e.entity_id }, "idx_audit_entity");

            entity.HasIndex(e => e.user_id, "idx_audit_user");

            entity.Property(e => e.audit_log_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.action).HasMaxLength(100);
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.entity_name).HasMaxLength(100);
            entity.Property(e => e.new_values).HasColumnType("jsonb");
            entity.Property(e => e.old_values).HasColumnType("jsonb");

            entity.HasOne(d => d.user).WithMany(p => p.audit_logs)
                .HasForeignKey(d => d.user_id)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("audit_logs_user_id_fkey");
        });

        modelBuilder.Entity<billing>(entity =>
        {
            entity.HasKey(e => e.billing_id).HasName("billing_pkey");

            entity.ToTable("billing", "billing");

            entity.HasIndex(e => e.issue_date, "idx_billing_issue_date");

            entity.HasIndex(e => e.patient_id, "idx_billing_patient");

            entity.HasIndex(e => e.status, "idx_billing_status");

            entity.Property(e => e.billing_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.deleted_at).HasColumnType("timestamp without time zone");
            entity.Property(e => e.discount)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0");
            entity.Property(e => e.insurance_coverage)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0");
            entity.Property(e => e.issue_date)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.status).HasMaxLength(30);
            entity.Property(e => e.subtotal).HasPrecision(12, 2);
            entity.Property(e => e.total_amount)
                .HasPrecision(12, 2)
                .HasComputedColumnSql("((subtotal - discount) - insurance_coverage)", true);
            entity.Property(e => e.updated_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.appointment).WithMany(p => p.billings)
                .HasForeignKey(d => d.appointment_id)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("billing_appointment_id_fkey");

            entity.HasOne(d => d.created_byNavigation).WithMany(p => p.billingcreated_byNavigations)
                .HasForeignKey(d => d.created_by)
                .HasConstraintName("billing_created_by_fkey");

            entity.HasOne(d => d.insurance_policy).WithMany(p => p.billings)
                .HasForeignKey(d => d.insurance_policy_id)
                .HasConstraintName("billing_insurance_policy_id_fkey");

            entity.HasOne(d => d.patient).WithMany(p => p.billings)
                .HasForeignKey(d => d.patient_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("billing_patient_id_fkey");

            entity.HasOne(d => d.updated_byNavigation).WithMany(p => p.billingupdated_byNavigations)
                .HasForeignKey(d => d.updated_by)
                .HasConstraintName("billing_updated_by_fkey");
        });

        modelBuilder.Entity<billing_detail>(entity =>
        {
            entity.HasKey(e => e.billing_detail_id).HasName("billing_details_pkey");

            entity.ToTable("billing_details", "billing");

            entity.Property(e => e.billing_detail_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.amount)
                .HasPrecision(12, 2)
                .HasComputedColumnSql("((quantity)::numeric * unit_price)", true);
            entity.Property(e => e.unit_price).HasPrecision(12, 2);

            entity.HasOne(d => d.billing).WithMany(p => p.billing_details)
                .HasForeignKey(d => d.billing_id)
                .HasConstraintName("billing_details_billing_id_fkey");
        });

        modelBuilder.Entity<department>(entity =>
        {
            entity.HasKey(e => e.department_id).HasName("departments_pkey");

            entity.ToTable("departments", "clinical");

            entity.HasIndex(e => e.name, "departments_name_key").IsUnique();

            entity.Property(e => e.department_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.is_active).HasDefaultValue(true);
            entity.Property(e => e.name).HasMaxLength(100);
            entity.Property(e => e.updated_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<diagnosis>(entity =>
        {
            entity.HasKey(e => e.diagnosis_id).HasName("diagnoses_pkey");

            entity.ToTable("diagnoses", "clinical");

            entity.Property(e => e.diagnosis_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.cie10_code).HasMaxLength(10);
            entity.Property(e => e.is_primary).HasDefaultValue(false);
            entity.Property(e => e.noted_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.medical_record).WithMany(p => p.diagnoses)
                .HasForeignKey(d => d.medical_record_id)
                .HasConstraintName("diagnoses_medical_record_id_fkey");
        });

        modelBuilder.Entity<doctor>(entity =>
        {
            entity.HasKey(e => e.doctor_id).HasName("doctors_pkey");

            entity.ToTable("doctors", "clinical");

            entity.HasIndex(e => e.license_number, "doctors_license_number_key").IsUnique();

            entity.HasIndex(e => e.user_id, "doctors_user_id_key").IsUnique();

            entity.Property(e => e.doctor_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.consultation_fee).HasPrecision(10, 2);
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.deleted_at).HasColumnType("timestamp without time zone");
            entity.Property(e => e.is_active).HasDefaultValue(true);
            entity.Property(e => e.license_number).HasMaxLength(50);
            entity.Property(e => e.office).HasMaxLength(100);
            entity.Property(e => e.updated_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.created_byNavigation).WithMany(p => p.doctorcreated_byNavigations)
                .HasForeignKey(d => d.created_by)
                .HasConstraintName("doctors_created_by_fkey");

            entity.HasOne(d => d.specialty).WithMany(p => p.doctors)
                .HasForeignKey(d => d.specialty_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doctors_specialty_id_fkey");

            entity.HasOne(d => d.updated_byNavigation).WithMany(p => p.doctorupdated_byNavigations)
                .HasForeignKey(d => d.updated_by)
                .HasConstraintName("doctors_updated_by_fkey");

            entity.HasOne(d => d.user).WithOne(p => p.doctoruser)
                .HasForeignKey<doctor>(d => d.user_id)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("doctors_user_id_fkey");
        });

        modelBuilder.Entity<insurance_company>(entity =>
        {
            entity.HasKey(e => e.insurance_company_id).HasName("insurance_companies_pkey");

            entity.ToTable("insurance_companies", "billing");

            entity.HasIndex(e => e.name, "insurance_companies_name_key").IsUnique();

            entity.Property(e => e.insurance_company_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.contact_name).HasMaxLength(100);
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.email).HasMaxLength(150);
            entity.Property(e => e.is_active).HasDefaultValue(true);
            entity.Property(e => e.name).HasMaxLength(150);
            entity.Property(e => e.phone).HasMaxLength(20);
            entity.Property(e => e.updated_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<insurance_policy>(entity =>
        {
            entity.HasKey(e => e.insurance_policy_id).HasName("insurance_policies_pkey");

            entity.ToTable("insurance_policies", "billing");

            entity.HasIndex(e => e.policy_number, "insurance_policies_policy_number_key").IsUnique();

            entity.Property(e => e.insurance_policy_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.coverage_percentage).HasPrecision(5, 2);
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.deleted_at).HasColumnType("timestamp without time zone");
            entity.Property(e => e.is_active).HasDefaultValue(true);
            entity.Property(e => e.max_coverage_amount).HasPrecision(12, 2);
            entity.Property(e => e.policy_number).HasMaxLength(100);
            entity.Property(e => e.updated_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.created_byNavigation).WithMany(p => p.insurance_policycreated_byNavigations)
                .HasForeignKey(d => d.created_by)
                .HasConstraintName("insurance_policies_created_by_fkey");

            entity.HasOne(d => d.insurance_company).WithMany(p => p.insurance_policies)
                .HasForeignKey(d => d.insurance_company_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("insurance_policies_insurance_company_id_fkey");

            entity.HasOne(d => d.patient).WithMany(p => p.insurance_policies)
                .HasForeignKey(d => d.patient_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("insurance_policies_patient_id_fkey");

            entity.HasOne(d => d.updated_byNavigation).WithMany(p => p.insurance_policyupdated_byNavigations)
                .HasForeignKey(d => d.updated_by)
                .HasConstraintName("insurance_policies_updated_by_fkey");
        });

        modelBuilder.Entity<laboratory_result>(entity =>
        {
            entity.HasKey(e => e.result_id).HasName("laboratory_results_pkey");

            entity.ToTable("laboratory_results", "laboratory");

            entity.Property(e => e.result_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.is_abnormal).HasDefaultValue(false);
            entity.Property(e => e.noted_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.parameter_name).HasMaxLength(100);
            entity.Property(e => e.reference_range).HasMaxLength(100);
            entity.Property(e => e.unit).HasMaxLength(50);

            entity.HasOne(d => d.laboratory_test).WithMany(p => p.laboratory_results)
                .HasForeignKey(d => d.laboratory_test_id)
                .HasConstraintName("laboratory_results_laboratory_test_id_fkey");
        });

        modelBuilder.Entity<laboratory_test>(entity =>
        {
            entity.HasKey(e => e.laboratory_test_id).HasName("laboratory_tests_pkey");

            entity.ToTable("laboratory_tests", "laboratory");

            entity.HasIndex(e => e.doctor_id, "idx_lab_tests_doctor");

            entity.HasIndex(e => e.patient_id, "idx_lab_tests_patient");

            entity.HasIndex(e => e.status, "idx_lab_tests_status");

            entity.Property(e => e.laboratory_test_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.completed_date).HasColumnType("timestamp without time zone");
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.deleted_at).HasColumnType("timestamp without time zone");
            entity.Property(e => e.requested_date)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.sample_taken_date).HasColumnType("timestamp without time zone");
            entity.Property(e => e.status).HasMaxLength(30);
            entity.Property(e => e.test_name).HasMaxLength(150);
            entity.Property(e => e.updated_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.created_byNavigation).WithMany(p => p.laboratory_testcreated_byNavigations)
                .HasForeignKey(d => d.created_by)
                .HasConstraintName("laboratory_tests_created_by_fkey");

            entity.HasOne(d => d.doctor).WithMany(p => p.laboratory_tests)
                .HasForeignKey(d => d.doctor_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("laboratory_tests_doctor_id_fkey");

            entity.HasOne(d => d.medical_record).WithMany(p => p.laboratory_tests)
                .HasForeignKey(d => d.medical_record_id)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("laboratory_tests_medical_record_id_fkey");

            entity.HasOne(d => d.patient).WithMany(p => p.laboratory_tests)
                .HasForeignKey(d => d.patient_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("laboratory_tests_patient_id_fkey");

            entity.HasOne(d => d.updated_byNavigation).WithMany(p => p.laboratory_testupdated_byNavigations)
                .HasForeignKey(d => d.updated_by)
                .HasConstraintName("laboratory_tests_updated_by_fkey");
        });

        modelBuilder.Entity<medical_record>(entity =>
        {
            entity.HasKey(e => e.medical_record_id).HasName("medical_records_pkey");

            entity.ToTable("medical_records", "clinical");

            entity.HasIndex(e => e.doctor_id, "idx_medical_records_doctor");

            entity.HasIndex(e => e.patient_id, "idx_medical_records_patient");

            entity.Property(e => e.medical_record_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.deleted_at).HasColumnType("timestamp without time zone");
            entity.Property(e => e.updated_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.appointment).WithMany(p => p.medical_records)
                .HasForeignKey(d => d.appointment_id)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("medical_records_appointment_id_fkey");

            entity.HasOne(d => d.created_byNavigation).WithMany(p => p.medical_recordcreated_byNavigations)
                .HasForeignKey(d => d.created_by)
                .HasConstraintName("medical_records_created_by_fkey");

            entity.HasOne(d => d.doctor).WithMany(p => p.medical_records)
                .HasForeignKey(d => d.doctor_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("medical_records_doctor_id_fkey");

            entity.HasOne(d => d.patient).WithMany(p => p.medical_records)
                .HasForeignKey(d => d.patient_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("medical_records_patient_id_fkey");

            entity.HasOne(d => d.updated_byNavigation).WithMany(p => p.medical_recordupdated_byNavigations)
                .HasForeignKey(d => d.updated_by)
                .HasConstraintName("medical_records_updated_by_fkey");
        });

        modelBuilder.Entity<medication>(entity =>
        {
            entity.HasKey(e => e.medication_id).HasName("medications_pkey");

            entity.ToTable("medications", "pharmacy");

            entity.Property(e => e.medication_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.concentration).HasMaxLength(50);
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.deleted_at).HasColumnType("timestamp without time zone");
            entity.Property(e => e.generic_name).HasMaxLength(150);
            entity.Property(e => e.is_active).HasDefaultValue(true);
            entity.Property(e => e.laboratory).HasMaxLength(100);
            entity.Property(e => e.name).HasMaxLength(150);
            entity.Property(e => e.presentation).HasMaxLength(100);
            entity.Property(e => e.requires_prescription).HasDefaultValue(true);
            entity.Property(e => e.stock).HasDefaultValue(0);
            entity.Property(e => e.unit_price).HasPrecision(10, 2);
            entity.Property(e => e.updated_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.created_byNavigation).WithMany(p => p.medicationcreated_byNavigations)
                .HasForeignKey(d => d.created_by)
                .HasConstraintName("medications_created_by_fkey");

            entity.HasOne(d => d.updated_byNavigation).WithMany(p => p.medicationupdated_byNavigations)
                .HasForeignKey(d => d.updated_by)
                .HasConstraintName("medications_updated_by_fkey");
        });

        modelBuilder.Entity<notification>(entity =>
        {
            entity.HasKey(e => e.notification_id).HasName("notifications_pkey");

            entity.ToTable("notifications", "notifications");

            entity.HasIndex(e => e.status, "idx_notifications_status");

            entity.HasIndex(e => e.user_id, "idx_notifications_user");

            entity.Property(e => e.notification_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.channel).HasMaxLength(20);
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.entity_type).HasMaxLength(50);
            entity.Property(e => e.read_at).HasColumnType("timestamp without time zone");
            entity.Property(e => e.scheduled_at).HasColumnType("timestamp without time zone");
            entity.Property(e => e.sent_at).HasColumnType("timestamp without time zone");
            entity.Property(e => e.status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pending'::character varying");

            entity.HasOne(d => d.type).WithMany(p => p.notifications)
                .HasForeignKey(d => d.type_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notifications_type_id_fkey");

            entity.HasOne(d => d.user).WithMany(p => p.notifications)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("notifications_user_id_fkey");
        });

        modelBuilder.Entity<notification_type>(entity =>
        {
            entity.HasKey(e => e.type_id).HasName("notification_types_pkey");

            entity.ToTable("notification_types", "notifications");

            entity.HasIndex(e => e.code, "notification_types_code_key").IsUnique();

            entity.Property(e => e.type_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.code).HasMaxLength(50);
            entity.Property(e => e.is_active).HasDefaultValue(true);
            entity.Property(e => e.name).HasMaxLength(100);
        });

        modelBuilder.Entity<patient>(entity =>
        {
            entity.HasKey(e => e.patient_id).HasName("patients_pkey");

            entity.ToTable("patients", "clinical");

            entity.HasIndex(e => e.document_number, "patients_document_number_key").IsUnique();

            entity.HasIndex(e => e.user_id, "patients_user_id_key").IsUnique();

            entity.Property(e => e.patient_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.blood_type).HasMaxLength(5);
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.deleted_at).HasColumnType("timestamp without time zone");
            entity.Property(e => e.document_number).HasMaxLength(20);
            entity.Property(e => e.email).HasMaxLength(150);
            entity.Property(e => e.emergency_contact_name).HasMaxLength(100);
            entity.Property(e => e.emergency_contact_phone).HasMaxLength(20);
            entity.Property(e => e.first_name).HasMaxLength(100);
            entity.Property(e => e.gender).HasMaxLength(20);
            entity.Property(e => e.is_active).HasDefaultValue(true);
            entity.Property(e => e.last_name).HasMaxLength(100);
            entity.Property(e => e.phone).HasMaxLength(20);
            entity.Property(e => e.updated_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.created_byNavigation).WithMany(p => p.patientcreated_byNavigations)
                .HasForeignKey(d => d.created_by)
                .HasConstraintName("patients_created_by_fkey");

            entity.HasOne(d => d.updated_byNavigation).WithMany(p => p.patientupdated_byNavigations)
                .HasForeignKey(d => d.updated_by)
                .HasConstraintName("patients_updated_by_fkey");

            entity.HasOne(d => d.user).WithOne(p => p.patientuser)
                .HasForeignKey<patient>(d => d.user_id)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("patients_user_id_fkey");
        });

        modelBuilder.Entity<payment>(entity =>
        {
            entity.HasKey(e => e.payment_id).HasName("payments_pkey");

            entity.ToTable("payments", "billing");

            entity.HasIndex(e => e.billing_id, "idx_payments_billing");

            entity.Property(e => e.payment_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.amount).HasPrecision(12, 2);
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.payment_date)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.payment_method).HasMaxLength(50);
            entity.Property(e => e.reference_number).HasMaxLength(100);
            entity.Property(e => e.status)
                .HasMaxLength(30)
                .HasDefaultValueSql("'pending'::character varying");

            entity.HasOne(d => d.billing).WithMany(p => p.payments)
                .HasForeignKey(d => d.billing_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payments_billing_id_fkey");

            entity.HasOne(d => d.insurance_policy).WithMany(p => p.payments)
                .HasForeignKey(d => d.insurance_policy_id)
                .HasConstraintName("payments_insurance_policy_id_fkey");

            entity.HasOne(d => d.registered_byNavigation).WithMany(p => p.payments)
                .HasForeignKey(d => d.registered_by)
                .HasConstraintName("payments_registered_by_fkey");
        });

        modelBuilder.Entity<permission>(entity =>
        {
            entity.HasKey(e => e.permission_id).HasName("permissions_pkey");

            entity.ToTable("permissions", "auth");

            entity.HasIndex(e => new { e.resource, e.action }, "permissions_resource_action_key").IsUnique();

            entity.Property(e => e.permission_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.action).HasMaxLength(50);
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.resource).HasMaxLength(100);
        });

        modelBuilder.Entity<prescription>(entity =>
        {
            entity.HasKey(e => e.prescription_id).HasName("prescriptions_pkey");

            entity.ToTable("prescriptions", "pharmacy");

            entity.HasIndex(e => e.medical_record_id, "idx_prescriptions_medical_record");

            entity.HasIndex(e => e.patient_id, "idx_prescriptions_patient");

            entity.Property(e => e.prescription_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.deleted_at).HasColumnType("timestamp without time zone");
            entity.Property(e => e.dispensed_at).HasColumnType("timestamp without time zone");
            entity.Property(e => e.updated_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.created_byNavigation).WithMany(p => p.prescriptioncreated_byNavigations)
                .HasForeignKey(d => d.created_by)
                .HasConstraintName("prescriptions_created_by_fkey");

            entity.HasOne(d => d.dispensed_byNavigation).WithMany(p => p.prescriptiondispensed_byNavigations)
                .HasForeignKey(d => d.dispensed_by)
                .HasConstraintName("prescriptions_dispensed_by_fkey");

            entity.HasOne(d => d.doctor).WithMany(p => p.prescriptions)
                .HasForeignKey(d => d.doctor_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prescriptions_doctor_id_fkey");

            entity.HasOne(d => d.medical_record).WithMany(p => p.prescriptions)
                .HasForeignKey(d => d.medical_record_id)
                .HasConstraintName("prescriptions_medical_record_id_fkey");

            entity.HasOne(d => d.patient).WithMany(p => p.prescriptions)
                .HasForeignKey(d => d.patient_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prescriptions_patient_id_fkey");

            entity.HasOne(d => d.updated_byNavigation).WithMany(p => p.prescriptionupdated_byNavigations)
                .HasForeignKey(d => d.updated_by)
                .HasConstraintName("prescriptions_updated_by_fkey");
        });

        modelBuilder.Entity<prescription_detail>(entity =>
        {
            entity.HasKey(e => e.prescription_detail_id).HasName("prescription_details_pkey");

            entity.ToTable("prescription_details", "pharmacy");

            entity.Property(e => e.prescription_detail_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.dosage).HasMaxLength(100);
            entity.Property(e => e.frequency).HasMaxLength(100);
            entity.Property(e => e.is_substitutable).HasDefaultValue(true);

            entity.HasOne(d => d.medication).WithMany(p => p.prescription_details)
                .HasForeignKey(d => d.medication_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prescription_details_medication_id_fkey");

            entity.HasOne(d => d.prescription).WithMany(p => p.prescription_details)
                .HasForeignKey(d => d.prescription_id)
                .HasConstraintName("prescription_details_prescription_id_fkey");
        });

        modelBuilder.Entity<refresh_token>(entity =>
        {
            entity.HasKey(e => e.refresh_token_id).HasName("refresh_tokens_pkey");

            entity.ToTable("refresh_tokens", "auth");

            entity.Property(e => e.refresh_token_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.expires_at).HasColumnType("timestamp without time zone");
            entity.Property(e => e.revoked_at).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.user).WithMany(p => p.refresh_tokens)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("refresh_tokens_user_id_fkey");
        });

        modelBuilder.Entity<role>(entity =>
        {
            entity.HasKey(e => e.role_id).HasName("roles_pkey");

            entity.ToTable("roles", "auth");

            entity.HasIndex(e => e.name, "roles_name_key").IsUnique();

            entity.Property(e => e.role_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.name).HasMaxLength(50);
            entity.Property(e => e.updated_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasMany(d => d.permissions).WithMany(p => p.roles)
                .UsingEntity<Dictionary<string, object>>(
                    "role_permission",
                    r => r.HasOne<permission>().WithMany()
                        .HasForeignKey("permission_id")
                        .HasConstraintName("role_permissions_permission_id_fkey"),
                    l => l.HasOne<role>().WithMany()
                        .HasForeignKey("role_id")
                        .HasConstraintName("role_permissions_role_id_fkey"),
                    j =>
                    {
                        j.HasKey("role_id", "permission_id").HasName("role_permissions_pkey");
                        j.ToTable("role_permissions", "auth");
                    });
        });

        modelBuilder.Entity<specialty>(entity =>
        {
            entity.HasKey(e => e.specialty_id).HasName("specialties_pkey");

            entity.ToTable("specialties", "clinical");

            entity.HasIndex(e => e.name, "specialties_name_key").IsUnique();

            entity.Property(e => e.specialty_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.is_active).HasDefaultValue(true);
            entity.Property(e => e.name).HasMaxLength(100);
            entity.Property(e => e.updated_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.department).WithMany(p => p.specialties)
                .HasForeignKey(d => d.department_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("specialties_department_id_fkey");
        });

        modelBuilder.Entity<stock_movement>(entity =>
        {
            entity.HasKey(e => e.movement_id).HasName("stock_movements_pkey");

            entity.ToTable("stock_movements", "pharmacy");

            entity.HasIndex(e => e.medication_id, "idx_stock_movements_medication");

            entity.Property(e => e.movement_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.movement_type).HasMaxLength(30);

            entity.HasOne(d => d.medication).WithMany(p => p.stock_movements)
                .HasForeignKey(d => d.medication_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stock_movements_medication_id_fkey");

            entity.HasOne(d => d.performed_byNavigation).WithMany(p => p.stock_movements)
                .HasForeignKey(d => d.performed_by)
                .HasConstraintName("stock_movements_performed_by_fkey");
        });

        modelBuilder.Entity<treatment>(entity =>
        {
            entity.HasKey(e => e.treatment_id).HasName("treatments_pkey");

            entity.ToTable("treatments", "clinical");

            entity.Property(e => e.treatment_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.status)
                .HasMaxLength(30)
                .HasDefaultValueSql("'active'::character varying");

            entity.HasOne(d => d.medical_record).WithMany(p => p.treatments)
                .HasForeignKey(d => d.medical_record_id)
                .HasConstraintName("treatments_medical_record_id_fkey");
        });

        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.user_id).HasName("users_pkey");

            entity.ToTable("users", "auth");

            entity.HasIndex(e => e.email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.username, "users_username_key").IsUnique();

            entity.Property(e => e.user_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.created_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.deleted_at).HasColumnType("timestamp without time zone");
            entity.Property(e => e.email).HasMaxLength(150);
            entity.Property(e => e.first_name).HasMaxLength(100);
            entity.Property(e => e.is_active).HasDefaultValue(true);
            entity.Property(e => e.last_name).HasMaxLength(100);
            entity.Property(e => e.phone).HasMaxLength(20);
            entity.Property(e => e.updated_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.username).HasMaxLength(100);

            entity.HasOne(d => d.created_byNavigation).WithMany(p => p.Inversecreated_byNavigation)
                .HasForeignKey(d => d.created_by)
                .HasConstraintName("users_created_by_fkey");

            entity.HasOne(d => d.role).WithMany(p => p.users)
                .HasForeignKey(d => d.role_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_id_fkey");

            entity.HasOne(d => d.updated_byNavigation).WithMany(p => p.Inverseupdated_byNavigation)
                .HasForeignKey(d => d.updated_by)
                .HasConstraintName("users_updated_by_fkey");
        });

        modelBuilder.Entity<vital_sign>(entity =>
        {
            entity.HasKey(e => e.vital_sign_id).HasName("vital_signs_pkey");

            entity.ToTable("vital_signs", "clinical");

            entity.Property(e => e.vital_sign_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.height_cm).HasPrecision(5, 2);
            entity.Property(e => e.recorded_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.temperature).HasPrecision(4, 1);
            entity.Property(e => e.weight_kg).HasPrecision(5, 2);

            entity.HasOne(d => d.medical_record).WithMany(p => p.vital_signs)
                .HasForeignKey(d => d.medical_record_id)
                .HasConstraintName("vital_signs_medical_record_id_fkey");

            entity.HasOne(d => d.recorded_byNavigation).WithMany(p => p.vital_signs)
                .HasForeignKey(d => d.recorded_by)
                .HasConstraintName("vital_signs_recorded_by_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
