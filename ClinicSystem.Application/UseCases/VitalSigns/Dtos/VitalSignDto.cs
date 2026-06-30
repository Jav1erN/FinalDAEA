namespace ClinicSystem.Application.UseCases.VitalSigns.Dtos;

public class VitalSignDto
{
    public Guid VitalSignId { get; set; } = Guid.Empty;

    public Guid MedicalRecordId { get; set; } = Guid.Empty;

    public Guid? RecordedBy { get; set; } = null;

    public int? SystolicBp { get; set; } = null;

    public int? DiastolicBp { get; set; } = null;

    public int? HeartRate { get; set; } = null;

    public decimal? Temperature { get; set; } = null;

    public int? RespiratoryRate { get; set; } = null;

    public decimal? WeightKg { get; set; } = null;

    public decimal? HeightCm { get; set; } = null;

    public int? Spo2 { get; set; } = null;

    public DateTime? RecordedAt { get; set; } = null;
}

