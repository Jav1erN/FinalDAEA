using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.Appointments.Commands;

public record DeleteAppointmentCommand(Guid AppointmentId) : IRequest<Result<bool>>;

public class DeleteAppointmentCommandHandler
    : IRequestHandler<DeleteAppointmentCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAppointmentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteAppointmentCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<Appointment>();
        var entity = await repository.GetByIdAsync(request.AppointmentId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("Appointment not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
