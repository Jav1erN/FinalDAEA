using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.BillingDetails.Commands;

public class CreateBillingDetailCommand : IRequest<Result<BillingDetailDto>>
{
    public Guid BillingId { get; set; } = Guid.Empty;

    public string Description { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }
}

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

        await _unitOfWork.BillingDetails.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<BillingDetailDto>.Success(entity.ToDto());
    }
}

