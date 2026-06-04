using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.Doctors.Commands;

public record DeleteDoctorCommand(Guid DoctorId) : IRequest<Result<bool>>;

public class DeleteDoctorCommandHandler
    : IRequestHandler<DeleteDoctorCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDoctorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteDoctorCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<Doctor>();
        var entity = await repository.GetByIdAsync(request.DoctorId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("Doctor not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
