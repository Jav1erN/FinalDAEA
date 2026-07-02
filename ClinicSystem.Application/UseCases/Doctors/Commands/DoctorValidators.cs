using FluentValidation;

namespace ClinicSystem.Application.UseCases.Doctors.Commands;

public class CreateDoctorValidator : AbstractValidator<CreateDoctorCommand>
{
    public CreateDoctorValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.SpecialtyId).NotEmpty();
        RuleFor(x => x.LicenseNumber).NotEmpty().MaximumLength(50);
        RuleFor(x => x.YearsExperience).GreaterThanOrEqualTo(0).When(x => x.YearsExperience.HasValue);
        RuleFor(x => x.ConsultationFee).GreaterThanOrEqualTo(0).When(x => x.ConsultationFee.HasValue);
        RuleFor(x => x.Office).MaximumLength(100);
    }
}

public class UpdateDoctorValidator : AbstractValidator<UpdateDoctorCommand>
{
    public UpdateDoctorValidator()
    {
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.SpecialtyId).NotEmpty();
        RuleFor(x => x.LicenseNumber).NotEmpty().MaximumLength(50);
        RuleFor(x => x.YearsExperience).GreaterThanOrEqualTo(0).When(x => x.YearsExperience.HasValue);
        RuleFor(x => x.ConsultationFee).GreaterThanOrEqualTo(0).When(x => x.ConsultationFee.HasValue);
        RuleFor(x => x.Office).MaximumLength(100);
    }
}

