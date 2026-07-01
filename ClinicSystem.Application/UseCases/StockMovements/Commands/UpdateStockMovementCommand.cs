using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using ClinicSystem.Domain.Ports.Services;
using MediatR;

namespace ClinicSystem.Application.UseCases.StockMovements.Commands;

public class UpdateStockMovementCommand : IRequest<Result<StockMovementDto>>
{
    public Guid MovementId { get; set; } = Guid.Empty;

    public Guid MedicationId { get; set; } = Guid.Empty;

    public string MovementType { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public Guid? ReferenceId { get; set; }

    public string? Notes { get; set; }

    public Guid? PerformedBy { get; set; }
}

public class UpdateStockMovementCommandHandler
    : IRequestHandler<UpdateStockMovementCommand, Result<StockMovementDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInventoryPolicyService _inventoryPolicyService;

    public UpdateStockMovementCommandHandler(
        IUnitOfWork unitOfWork,
        IInventoryPolicyService inventoryPolicyService)
    {
        _unitOfWork = unitOfWork;
        _inventoryPolicyService = inventoryPolicyService;
    }

    public async Task<Result<StockMovementDto>> Handle(
        UpdateStockMovementCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.StockMovements;
        var entity = await repository.GetByIdAsync(request.MovementId, cancellationToken);

        if (entity is null)
            return Result<StockMovementDto>.Failure("StockMovement not found");

        var inventoryResult = await _inventoryPolicyService.EnsureCanApplyMovementAsync(
            request.MedicationId,
            request.MovementType,
            request.Quantity,
            cancellationToken);

        if (!inventoryResult.IsValid)
            return Result<StockMovementDto>.Failure(inventoryResult.Error!);

        entity.MedicationId = request.MedicationId;
        entity.MovementType = request.MovementType;
        entity.Quantity = request.Quantity;
        entity.ReferenceId = request.ReferenceId;
        entity.Notes = request.Notes;
        entity.PerformedBy = request.PerformedBy;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<StockMovementDto>.Success(entity.ToDto());
    }
}

