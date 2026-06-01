using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class AppointmentStatus
{
    public Guid StatusId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
