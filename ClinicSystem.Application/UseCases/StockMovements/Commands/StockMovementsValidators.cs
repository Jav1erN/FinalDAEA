using FluentValidation;

namespace ClinicSystem.Application.UseCases.StockMovements.Commands;

public class CreateStockMovementValidator : AbstractValidator<CreateStockMovementCommand>
{
    public CreateStockMovementValidator()
    {
        RuleFor(x => x.MedicationId).NotEmpty();
        RuleFor(x => x.MovementType).NotEmpty();
    }
}
public class UpdateStockMovementValidator : AbstractValidator<UpdateStockMovementCommand>
{
    public UpdateStockMovementValidator()
    {
        RuleFor(x => x.MovementId).NotEmpty();
        RuleFor(x => x.MedicationId).NotEmpty();
        RuleFor(x => x.MovementType).NotEmpty();
    }
}
