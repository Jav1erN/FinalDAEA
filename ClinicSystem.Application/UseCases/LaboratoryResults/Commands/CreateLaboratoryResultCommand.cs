using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.LaboratoryResults.Commands;

public class CreateLaboratoryResultCommand : IRequest<Result<LaboratoryResultDto>>
{
    public Guid LaboratoryTestId { get; set; } = Guid.Empty;

    public string ParameterName { get; set; } = string.Empty;

    public string? ResultValue { get; set; }

    public string? Unit { get; set; }

    public string? ReferenceRange { get; set; }

    public bool? IsAbnormal { get; set; }

    public DateTime? NotedAt { get; set; }
}

public class CreateLaboratoryResultCommandHandler
    : IRequestHandler<CreateLaboratoryResultCommand, Result<LaboratoryResultDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateLaboratoryResultCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<LaboratoryResultDto>> Handle(
        CreateLaboratoryResultCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new LaboratoryResult
        {
            ResultId = Guid.NewGuid(),
            LaboratoryTestId = request.LaboratoryTestId,
            ParameterName = request.ParameterName,
            ResultValue = request.ResultValue,
            Unit = request.Unit,
            ReferenceRange = request.ReferenceRange,
            IsAbnormal = request.IsAbnormal,
            NotedAt = request.NotedAt
        };

        await _unitOfWork.LaboratoryResults.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<LaboratoryResultDto>.Success(entity.ToDto());
    }
}

