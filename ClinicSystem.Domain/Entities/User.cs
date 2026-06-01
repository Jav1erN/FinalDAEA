using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class User
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual ICollection<Appointment> AppointmentCreatedByNavigations { get; set; } = new List<Appointment>();

    public virtual ICollection<Appointment> AppointmentUpdatedByNavigations { get; set; } = new List<Appointment>();

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<Billing> BillingCreatedByNavigations { get; set; } = new List<Billing>();

    public virtual ICollection<Billing> BillingUpdatedByNavigations { get; set; } = new List<Billing>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Doctor> DoctorCreatedByNavigations { get; set; } = new List<Doctor>();

    public virtual ICollection<Doctor> DoctorUpdatedByNavigations { get; set; } = new List<Doctor>();

    public virtual Doctor? DoctorUser { get; set; }

    public virtual ICollection<InsurancePolicy> InsurancePolicyCreatedByNavigations { get; set; } = new List<InsurancePolicy>();

    public virtual ICollection<InsurancePolicy> InsurancePolicyUpdatedByNavigations { get; set; } = new List<InsurancePolicy>();

    public virtual ICollection<User> InverseCreatedByNavigation { get; set; } = new List<User>();

    public virtual ICollection<User> InverseUpdatedByNavigation { get; set; } = new List<User>();

    public virtual ICollection<LaboratoryTest> LaboratoryTestCreatedByNavigations { get; set; } = new List<LaboratoryTest>();

    public virtual ICollection<LaboratoryTest> LaboratoryTestUpdatedByNavigations { get; set; } = new List<LaboratoryTest>();

    public virtual ICollection<MedicalRecord> MedicalRecordCreatedByNavigations { get; set; } = new List<MedicalRecord>();

    public virtual ICollection<MedicalRecord> MedicalRecordUpdatedByNavigations { get; set; } = new List<MedicalRecord>();

    public virtual ICollection<Medication> MedicationCreatedByNavigations { get; set; } = new List<Medication>();

    public virtual ICollection<Medication> MedicationUpdatedByNavigations { get; set; } = new List<Medication>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Patient> PatientCreatedByNavigations { get; set; } = new List<Patient>();

    public virtual ICollection<Patient> PatientUpdatedByNavigations { get; set; } = new List<Patient>();

    public virtual Patient? PatientUser { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Prescription> PrescriptionCreatedByNavigations { get; set; } = new List<Prescription>();

    public virtual ICollection<Prescription> PrescriptionDispensedByNavigations { get; set; } = new List<Prescription>();

    public virtual ICollection<Prescription> PrescriptionUpdatedByNavigations { get; set; } = new List<Prescription>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();

    public virtual User? UpdatedByNavigation { get; set; }

    public virtual ICollection<VitalSign> VitalSigns { get; set; } = new List<VitalSign>();
}
