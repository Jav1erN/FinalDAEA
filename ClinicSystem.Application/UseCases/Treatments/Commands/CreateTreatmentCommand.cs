using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Application.UseCases.Treatments.Dtos;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.Treatments.Commands;

public record CreateTreatmentCommand(
    Guid MedicalRecordId,
    string Description,
    DateOnly? StartDate,
    DateOnly? EndDate,
    string? Status,
    string? Notes
) : IRequest<Result<TreatmentDto>>;

public class CreateTreatmentCommandHandler
    : IRequestHandler<CreateTreatmentCommand, Result<TreatmentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTreatmentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TreatmentDto>> Handle(
        CreateTreatmentCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new Treatment
        {
            TreatmentId = Guid.NewGuid(),
            MedicalRecordId = request.MedicalRecordId,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = request.Status,
            Notes = request.Notes
        };

        await _unitOfWork.Repository<Treatment>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<TreatmentDto>.Success(entity.ToDto());
    }
}
