using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Payments.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Payments.Queries;

public record GetPaymentByIdQuery(Guid PaymentId) : IRequest<Result<PaymentDto>>;

public class GetPaymentByIdQueryHandler
    : IRequestHandler<GetPaymentByIdQuery, Result<PaymentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPaymentByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PaymentDto>> Handle(
        GetPaymentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<Payment>()
            .GetByIdAsync(request.PaymentId, cancellationToken);

        if (entity is null)
            return Result<PaymentDto>.Failure("Payment not found");

        return Result<PaymentDto>.Success(entity.ToDto());
    }
}
