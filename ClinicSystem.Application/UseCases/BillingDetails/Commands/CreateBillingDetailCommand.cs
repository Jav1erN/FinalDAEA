using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.BillingDetails.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.BillingDetails.Commands;

public record CreateBillingDetailCommand(
    Guid BillingId,
    string Description,
    int Quantity,
    decimal UnitPrice
) : IRequest<Result<BillingDetailDto>>;

public class CreateBillingDetailCommandHandler
    : IRequestHandler<CreateBillingDetailCommand, Result<BillingDetailDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateBillingDetailCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<BillingDetailDto>> Handle(
        CreateBillingDetailCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new BillingDetail
        {
            BillingDetailId = Guid.NewGuid(),
            BillingId = request.BillingId,
            Description = request.Description,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice
        };

        await _unitOfWork.Repository<BillingDetail>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<BillingDetailDto>.Success(entity.ToDto());
    }
}
