using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using ClinicSystem.Domain.Ports.Services;
using MediatR;

namespace ClinicSystem.Application.UseCases.StockMovements.Commands;

public class CreateStockMovementCommand : IRequest<Result<StockMovementDto>>
{
    public Guid MedicationId { get; set; } = Guid.Empty;

    public string MovementType { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public Guid? ReferenceId { get; set; }

    public string? Notes { get; set; }

    public Guid? PerformedBy { get; set; }
}

public class CreateStockMovementCommandHandler
    : IRequestHandler<CreateStockMovementCommand, Result<StockMovementDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInventoryPolicyService _inventoryPolicyService;

    public CreateStockMovementCommandHandler(
        IUnitOfWork unitOfWork,
        IInventoryPolicyService inventoryPolicyService)
    {
        _unitOfWork = unitOfWork;
        _inventoryPolicyService = inventoryPolicyService;
    }

    public async Task<Result<StockMovementDto>> Handle(
        CreateStockMovementCommand request,
        CancellationToken cancellationToken)
    {
        var inventoryResult = await _inventoryPolicyService.EnsureCanApplyMovementAsync(
            request.MedicationId,
            request.MovementType,
            request.Quantity,
            cancellationToken);

        if (!inventoryResult.IsValid)
            return Result<StockMovementDto>.Failure(inventoryResult.Error!);

        var entity = new StockMovement
        {
            MovementId = Guid.NewGuid(),
            MedicationId = request.MedicationId,
            MovementType = request.MovementType,
            Quantity = request.Quantity,
            ReferenceId = request.ReferenceId,
            Notes = request.Notes,
            PerformedBy = request.PerformedBy
        };

        await _unitOfWork.StockMovements.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<StockMovementDto>.Success(entity.ToDto());
    }
}

