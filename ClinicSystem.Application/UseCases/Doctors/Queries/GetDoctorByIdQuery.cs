using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Doctors.Queries;

public record GetDoctorByIdQuery(Guid DoctorId) : IRequest<Result<DoctorDto>>;

public class GetDoctorByIdQueryHandler
    : IRequestHandler<GetDoctorByIdQuery, Result<DoctorDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDoctorByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DoctorDto>> Handle(
        GetDoctorByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Doctors
            .GetByIdAsync(request.DoctorId, cancellationToken);

        if (entity is null)
            return Result<DoctorDto>.Failure("Doctor not found");

        return Result<DoctorDto>.Success(entity.ToDto());
    }
}

