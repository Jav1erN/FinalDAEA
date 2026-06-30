using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Patients.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Patients.Queries;

public record GetPatientsQuery : IRequest<Result<IEnumerable<PatientDto>>>;

public class GetPatientsQueryHandler
    : IRequestHandler<GetPatientsQuery, Result<IEnumerable<PatientDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPatientsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<PatientDto>>> Handle(
        GetPatientsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Patients
            .ListAsync(cancellationToken);

        return Result<IEnumerable<PatientDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}

