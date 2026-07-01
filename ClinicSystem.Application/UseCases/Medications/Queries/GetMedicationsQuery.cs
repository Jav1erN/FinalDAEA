using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Medications.Queries;

public record GetMedicationsQuery : IRequest<Result<IEnumerable<MedicationDto>>>;

public class GetMedicationsQueryHandler
    : IRequestHandler<GetMedicationsQuery, Result<IEnumerable<MedicationDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMedicationsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<MedicationDto>>> Handle(
        GetMedicationsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Medications
            .ListAsync(cancellationToken);

        return Result<IEnumerable<MedicationDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}

