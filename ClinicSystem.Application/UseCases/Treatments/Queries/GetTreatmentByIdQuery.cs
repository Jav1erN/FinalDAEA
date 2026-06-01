using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Application.UseCases.Treatments.Dtos;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.Treatments.Queries;

public record GetTreatmentByIdQuery(Guid TreatmentId) : IRequest<Result<TreatmentDto>>;

public class GetTreatmentByIdQueryHandler
    : IRequestHandler<GetTreatmentByIdQuery, Result<TreatmentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTreatmentByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TreatmentDto>> Handle(
        GetTreatmentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<Treatment>()
            .GetByIdAsync(request.TreatmentId, cancellationToken);

        if (entity is null)
            return Result<TreatmentDto>.Failure("Treatment not found");

        return Result<TreatmentDto>.Success(entity.ToDto());
    }
}
