using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Medications.Dtos;
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
        var entities = await _unitOfWork.Repository<Medication>()
            .ListAsync(cancellationToken);

        var medications = entities
            .Where(x => x.DeletedAt == null)
            .Select(x => x.ToDto());

        return Result<IEnumerable<MedicationDto>>
            .Success(medications);
    }
}