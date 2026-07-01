using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.LaboratoryResults.Commands;

public class UpdateLaboratoryResultCommand : IRequest<Result<LaboratoryResultDto>>
{
    public Guid ResultId { get; set; } = Guid.Empty;

    public Guid LaboratoryTestId { get; set; } = Guid.Empty;

    public string ParameterName { get; set; } = string.Empty;

    public string? ResultValue { get; set; }

    public string? Unit { get; set; }

    public string? ReferenceRange { get; set; }

    public bool? IsAbnormal { get; set; }

    public DateTime? NotedAt { get; set; }
}

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
        var repository = _unitOfWork.LaboratoryResults;
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

