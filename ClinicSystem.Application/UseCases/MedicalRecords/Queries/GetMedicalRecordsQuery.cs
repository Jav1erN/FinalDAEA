using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.MedicalRecords.Queries;

public record GetMedicalRecordsQuery : IRequest<Result<IEnumerable<MedicalRecordDto>>>;

public class GetMedicalRecordsQueryHandler
    : IRequestHandler<GetMedicalRecordsQuery, Result<IEnumerable<MedicalRecordDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMedicalRecordsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<MedicalRecordDto>>> Handle(
        GetMedicalRecordsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.MedicalRecords
            .ListAsync(cancellationToken);

        return Result<IEnumerable<MedicalRecordDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}

