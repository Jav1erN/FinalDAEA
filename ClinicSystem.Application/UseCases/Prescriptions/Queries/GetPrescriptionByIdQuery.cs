using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Prescriptions.Queries;

public record GetPrescriptionByIdQuery(Guid PrescriptionId) : IRequest<Result<PrescriptionDto>>;

public class GetPrescriptionByIdQueryHandler
    : IRequestHandler<GetPrescriptionByIdQuery, Result<PrescriptionDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPrescriptionByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PrescriptionDto>> Handle(
        GetPrescriptionByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Prescriptions
            .GetByIdAsync(request.PrescriptionId, cancellationToken);

        if (entity is null)
            return Result<PrescriptionDto>.Failure("Prescription not found");

        return Result<PrescriptionDto>.Success(entity.ToDto());
    }
}

