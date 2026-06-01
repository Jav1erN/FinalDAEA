using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Application.UseCases.LaboratoryResults.Dtos;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.LaboratoryResults.Commands;

public record UpdateLaboratoryResultCommand(
    Guid ResultId,
    Guid LaboratoryTestId,
    string ParameterName,
    string? ResultValue,
    string? Unit,
    string? ReferenceRange,
    bool? IsAbnormal,
    DateTime? NotedAt
) : IRequest<Result<LaboratoryResultDto>>;

public class UpdateLaboratoryResultCommandHandler
    : IRequestHandler<UpdateLaboratoryResultCommand, Result<LaboratoryResultDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLaboratoryResultCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<LaboratoryResultDto>> Handle(
        UpdateLaboratoryResultCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<LaboratoryResult>();
        var entity = await repository.GetByIdAsync(request.ResultId, cancellationToken);

        if (entity is null)
            return Result<LaboratoryResultDto>.Failure("LaboratoryResult not found");

        entity.LaboratoryTestId = request.LaboratoryTestId;
        entity.ParameterName = request.ParameterName;
        entity.ResultValue = request.ResultValue;
        entity.Unit = request.Unit;
        entity.ReferenceRange = request.ReferenceRange;
        entity.IsAbnormal = request.IsAbnormal;
        entity.NotedAt = request.NotedAt;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<LaboratoryResultDto>.Success(entity.ToDto());
    }
}
