using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.BillingDetails.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.BillingDetails.Commands;

public record UpdateBillingDetailCommand(
    Guid BillingDetailId,
    Guid BillingId,
    string Description,
    int Quantity,
    decimal UnitPrice
) : IRequest<Result<BillingDetailDto>>;

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
        var repository = _unitOfWork.Repository<BillingDetail>();
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
