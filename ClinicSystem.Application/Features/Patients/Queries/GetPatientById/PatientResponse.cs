namespace ClinicSystem.Application.Features.Patients.Queries.GetPatientById;

public class PatientResponse
{
    public Guid PatientId { get; set; }

    public string FullName { get; set; } = string.Empty;
}