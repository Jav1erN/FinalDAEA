using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.VitalSigns.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.VitalSigns.Queries;

public record GetVitalSignByIdQuery(Guid VitalSignId) : IRequest<Result<VitalSignDto>>;

public class GetVitalSignByIdQueryHandler
    : IRequestHandler<GetVitalSignByIdQuery, Result<VitalSignDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetVitalSignByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<VitalSignDto>> Handle(
        GetVitalSignByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.VitalSigns
            .GetByIdAsync(request.VitalSignId, cancellationToken);

        if (entity is null)
            return Result<VitalSignDto>.Failure("VitalSign not found");

        return Result<VitalSignDto>.Success(entity.ToDto());
    }
}

