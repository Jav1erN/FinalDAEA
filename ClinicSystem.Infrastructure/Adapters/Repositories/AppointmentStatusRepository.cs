using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using ClinicSystem.Infrastructure.Persistence.Context;

namespace ClinicSystem.Infrastructure.Persistence.Repositories;

public class AppointmentStatusRepository : Repository<AppointmentStatus>, IAppointmentStatusRepository
{
    public AppointmentStatusRepository(ClinicDbContext context) : base(context)
    {
    }
}
