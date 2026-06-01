using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class Doctor
{
    public Guid DoctorId { get; set; }

    public Guid UserId { get; set; }

    public Guid SpecialtyId { get; set; }

    public string LicenseNumber { get; set; } = null!;

    public int? YearsExperience { get; set; }

    public decimal? ConsultationFee { get; set; }

    public string? Office { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<LaboratoryTest> LaboratoryTests { get; set; } = new List<LaboratoryTest>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual Specialty Specialty { get; set; } = null!;

    public virtual User? UpdatedByNavigation { get; set; }

    public virtual User User { get; set; } = null!;
}
