using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.PrescriptionDetails.Queries;

public record GetPrescriptionDetailsQuery : IRequest<Result<IEnumerable<PrescriptionDetailDto>>>;

public class GetPrescriptionDetailsQueryHandler
    : IRequestHandler<GetPrescriptionDetailsQuery, Result<IEnumerable<PrescriptionDetailDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPrescriptionDetailsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<PrescriptionDetailDto>>> Handle(
        GetPrescriptionDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.PrescriptionDetails
            .ListAsync(cancellationToken);

        return Result<IEnumerable<PrescriptionDetailDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}

