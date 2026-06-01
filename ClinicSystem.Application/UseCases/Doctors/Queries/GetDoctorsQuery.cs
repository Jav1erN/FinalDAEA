using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Application.UseCases.Doctors.Dtos;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.Doctors.Queries;

public record GetDoctorsQuery : IRequest<Result<IEnumerable<DoctorDto>>>;

public class GetDoctorsQueryHandler
    : IRequestHandler<GetDoctorsQuery, Result<IEnumerable<DoctorDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDoctorsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<DoctorDto>>> Handle(
        GetDoctorsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Repository<Doctor>()
            .ListAsync(cancellationToken);

        return Result<IEnumerable<DoctorDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}
