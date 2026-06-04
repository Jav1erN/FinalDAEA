using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Appointments.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.Appointments.Queries;

public record GetAppointmentsQuery : IRequest<Result<IEnumerable<AppointmentDto>>>;

public class GetAppointmentsQueryHandler
    : IRequestHandler<GetAppointmentsQuery, Result<IEnumerable<AppointmentDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<AppointmentDto>>> Handle(
        GetAppointmentsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Repository<Appointment>()
            .ListAsync(cancellationToken);

        return Result<IEnumerable<AppointmentDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}
