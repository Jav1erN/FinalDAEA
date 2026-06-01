using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class MedicalRecord
{
    public Guid MedicalRecordId { get; set; }

    public Guid PatientId { get; set; }

    public Guid DoctorId { get; set; }

    public Guid? AppointmentId { get; set; }

    public string? ChiefComplaint { get; set; }

    public string? Diagnosis { get; set; }

    public string? Treatment { get; set; }

    public string? Observations { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual ICollection<LaboratoryTest> LaboratoryTests { get; set; } = new List<LaboratoryTest>();

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();

    public virtual User? UpdatedByNavigation { get; set; }

    public virtual ICollection<VitalSign> VitalSigns { get; set; } = new List<VitalSign>();
}
