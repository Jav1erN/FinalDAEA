using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Patients.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.Patients.Queries;

public record GetPatientByIdQuery(Guid PatientId) : IRequest<Result<PatientDto>>;

public class GetPatientByIdQueryHandler
    : IRequestHandler<GetPatientByIdQuery, Result<PatientDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPatientByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PatientDto>> Handle(
        GetPatientByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<Patient>()
            .GetByIdAsync(request.PatientId, cancellationToken);

        if (entity is null)
            return Result<PatientDto>.Failure("Patient not found");

        return Result<PatientDto>.Success(entity.ToDto());
    }
}
