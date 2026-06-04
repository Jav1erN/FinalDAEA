using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Payments.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Payments.Queries;

public record GetPaymentsQuery : IRequest<Result<IEnumerable<PaymentDto>>>;

public class GetPaymentsQueryHandler
    : IRequestHandler<GetPaymentsQuery, Result<IEnumerable<PaymentDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPaymentsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<PaymentDto>>> Handle(
        GetPaymentsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Repository<Payment>()
            .ListAsync(cancellationToken);

        return Result<IEnumerable<PaymentDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}
