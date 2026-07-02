using ClinicSystem.Domain.Common;

namespace ClinicSystem.Domain.Ports.Services;

public interface IInventoryPolicyService
{
    Task<BusinessRuleResult> EnsureCanApplyMovementAsync(
        Guid medicationId,
        string movementType,
        int quantity,
        CancellationToken cancellationToken = default);
}
