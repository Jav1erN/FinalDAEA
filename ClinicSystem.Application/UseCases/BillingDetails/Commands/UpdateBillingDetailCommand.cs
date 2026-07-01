using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.BillingDetails.Commands;

public class UpdateBillingDetailCommand : IRequest<Result<BillingDetailDto>>
{
    public Guid BillingDetailId { get; set; } = Guid.Empty;

    public Guid BillingId { get; set; } = Guid.Empty;

    public string Description { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }
}

public class UpdateBillingDetailCommandHandler
    : IRequestHandler<UpdateBillingDetailCommand, Result<BillingDetailDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBillingDetailCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<BillingDetailDto>> Handle(
        UpdateBillingDetailCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.BillingDetails;
        var entity = await repository.GetByIdAsync(request.BillingDetailId, cancellationToken);

        if (entity is null)
            return Result<BillingDetailDto>.Failure("BillingDetail not found");

        entity.BillingId = request.BillingId;
        entity.Description = request.Description;
        entity.Quantity = request.Quantity;
        entity.UnitPrice = request.UnitPrice;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<BillingDetailDto>.Success(entity.ToDto());
    }
}

