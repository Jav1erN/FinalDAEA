using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class Patient
{
    public Guid PatientId { get; set; }

    public Guid? UserId { get; set; }

    public string DocumentNumber { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly? BirthDate { get; set; }

    public string? Gender { get; set; }

    public string? BloodType { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? EmergencyContactName { get; set; }

    public string? EmergencyContactPhone { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Billing> Billings { get; set; } = new List<Billing>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<InsurancePolicy> InsurancePolicies { get; set; } = new List<InsurancePolicy>();

    public virtual ICollection<LaboratoryTest> LaboratoryTests { get; set; } = new List<LaboratoryTest>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual User? UpdatedByNavigation { get; set; }

    public virtual User? User { get; set; }
}
