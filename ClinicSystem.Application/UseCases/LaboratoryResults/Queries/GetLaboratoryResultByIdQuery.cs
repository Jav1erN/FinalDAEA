using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.LaboratoryResults.Queries;

public record GetLaboratoryResultByIdQuery(Guid ResultId) : IRequest<Result<LaboratoryResultDto>>;

public class GetLaboratoryResultByIdQueryHandler
    : IRequestHandler<GetLaboratoryResultByIdQuery, Result<LaboratoryResultDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetLaboratoryResultByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<LaboratoryResultDto>> Handle(
        GetLaboratoryResultByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.LaboratoryResults
            .GetByIdAsync(request.ResultId, cancellationToken);

        if (entity is null)
            return Result<LaboratoryResultDto>.Failure("LaboratoryResult not found");

        return Result<LaboratoryResultDto>.Success(entity.ToDto());
    }
}

