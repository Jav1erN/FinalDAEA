using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.PrescriptionDetails.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.PrescriptionDetails.Queries;

public record GetPrescriptionDetailByIdQuery(Guid PrescriptionDetailId) : IRequest<Result<PrescriptionDetailDto>>;

public class GetPrescriptionDetailByIdQueryHandler
    : IRequestHandler<GetPrescriptionDetailByIdQuery, Result<PrescriptionDetailDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPrescriptionDetailByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PrescriptionDetailDto>> Handle(
        GetPrescriptionDetailByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<PrescriptionDetail>()
            .GetByIdAsync(request.PrescriptionDetailId, cancellationToken);

        if (entity is null)
            return Result<PrescriptionDetailDto>.Failure("PrescriptionDetail not found");

        return Result<PrescriptionDetailDto>.Success(entity.ToDto());
    }
}
