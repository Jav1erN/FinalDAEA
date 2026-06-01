using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Application.UseCases.LaboratoryResults.Dtos;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.LaboratoryResults.Commands;

public record CreateLaboratoryResultCommand(
    Guid LaboratoryTestId,
    string ParameterName,
    string? ResultValue,
    string? Unit,
    string? ReferenceRange,
    bool? IsAbnormal,
    DateTime? NotedAt
) : IRequest<Result<LaboratoryResultDto>>;

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

        await _unitOfWork.Repository<LaboratoryResult>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<LaboratoryResultDto>.Success(entity.ToDto());
    }
}
