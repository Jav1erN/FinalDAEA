using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Patients.Commands;

public record DeletePatientCommand(Guid PatientId) : IRequest<Result<bool>>;

public class DeletePatientCommandHandler
    : IRequestHandler<DeletePatientCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePatientCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeletePatientCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<Patient>();
        var entity = await repository.GetByIdAsync(request.PatientId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("Patient not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
