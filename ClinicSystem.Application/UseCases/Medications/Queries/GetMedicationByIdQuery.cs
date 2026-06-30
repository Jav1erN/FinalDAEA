using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Medications.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Medications.Queries;

public record GetMedicationByIdQuery(Guid MedicationId) : IRequest<Result<MedicationDto>>;

public class GetMedicationByIdQueryHandler
    : IRequestHandler<GetMedicationByIdQuery, Result<MedicationDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMedicationByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<MedicationDto>> Handle(
        GetMedicationByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Medications
            .GetByIdAsync(request.MedicationId, cancellationToken);

        if (entity is null)
            return Result<MedicationDto>.Failure("Medication not found");

        return Result<MedicationDto>.Success(entity.ToDto());
    }
}

