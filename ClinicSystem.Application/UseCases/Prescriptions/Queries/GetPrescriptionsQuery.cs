using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Prescriptions.Queries;

public record GetPrescriptionsQuery : IRequest<Result<IEnumerable<PrescriptionDto>>>;

public class GetPrescriptionsQueryHandler
    : IRequestHandler<GetPrescriptionsQuery, Result<IEnumerable<PrescriptionDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPrescriptionsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<PrescriptionDto>>> Handle(
        GetPrescriptionsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Prescriptions
            .ListAsync(cancellationToken);

        return Result<IEnumerable<PrescriptionDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}

