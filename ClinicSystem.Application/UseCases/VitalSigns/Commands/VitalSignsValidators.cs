using FluentValidation;

namespace ClinicSystem.Application.UseCases.VitalSigns.Commands;

public class CreateVitalSignValidator : AbstractValidator<CreateVitalSignCommand>
{
    public CreateVitalSignValidator()
    {
        RuleFor(x => x.MedicalRecordId).NotEmpty();
        RuleFor(x => x.SystolicBp).GreaterThanOrEqualTo(0).When(x => x.SystolicBp.HasValue);
        RuleFor(x => x.DiastolicBp).GreaterThanOrEqualTo(0).When(x => x.DiastolicBp.HasValue);
        RuleFor(x => x.HeartRate).GreaterThanOrEqualTo(0).When(x => x.HeartRate.HasValue);
        RuleFor(x => x.Temperature).GreaterThanOrEqualTo(0).When(x => x.Temperature.HasValue);
        RuleFor(x => x.RespiratoryRate).GreaterThanOrEqualTo(0).When(x => x.RespiratoryRate.HasValue);
        RuleFor(x => x.WeightKg).GreaterThanOrEqualTo(0).When(x => x.WeightKg.HasValue);
        RuleFor(x => x.HeightCm).GreaterThanOrEqualTo(0).When(x => x.HeightCm.HasValue);
        RuleFor(x => x.Spo2).GreaterThanOrEqualTo(0).When(x => x.Spo2.HasValue);
    }
}
public class UpdateVitalSignValidator : AbstractValidator<UpdateVitalSignCommand>
{
    public UpdateVitalSignValidator()
    {
        RuleFor(x => x.VitalSignId).NotEmpty();
        RuleFor(x => x.MedicalRecordId).NotEmpty();
        RuleFor(x => x.SystolicBp).GreaterThanOrEqualTo(0).When(x => x.SystolicBp.HasValue);
        RuleFor(x => x.DiastolicBp).GreaterThanOrEqualTo(0).When(x => x.DiastolicBp.HasValue);
        RuleFor(x => x.HeartRate).GreaterThanOrEqualTo(0).When(x => x.HeartRate.HasValue);
        RuleFor(x => x.Temperature).GreaterThanOrEqualTo(0).When(x => x.Temperature.HasValue);
        RuleFor(x => x.RespiratoryRate).GreaterThanOrEqualTo(0).When(x => x.RespiratoryRate.HasValue);
        RuleFor(x => x.WeightKg).GreaterThanOrEqualTo(0).When(x => x.WeightKg.HasValue);
        RuleFor(x => x.HeightCm).GreaterThanOrEqualTo(0).When(x => x.HeightCm.HasValue);
        RuleFor(x => x.Spo2).GreaterThanOrEqualTo(0).When(x => x.Spo2.HasValue);
    }
}
