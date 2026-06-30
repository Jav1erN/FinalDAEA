using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.MedicalRecords.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.MedicalRecords.Queries;

public record GetMedicalRecordByIdQuery(Guid MedicalRecordId) : IRequest<Result<MedicalRecordDto>>;

public class GetMedicalRecordByIdQueryHandler
    : IRequestHandler<GetMedicalRecordByIdQuery, Result<MedicalRecordDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMedicalRecordByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<MedicalRecordDto>> Handle(
        GetMedicalRecordByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.MedicalRecords
            .GetByIdAsync(request.MedicalRecordId, cancellationToken);

        if (entity is null)
            return Result<MedicalRecordDto>.Failure("MedicalRecord not found");

        return Result<MedicalRecordDto>.Success(entity.ToDto());
    }
}

