using ClinicSystem.Domain.Common;
using ClinicSystem.Domain.Ports.Services;
using ClinicSystem.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ClinicSystem.Infrastructure.Adapters.Services;

public class InventoryPolicyService : IInventoryPolicyService
{
    private readonly ClinicDbContext _context;

    public InventoryPolicyService(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task<BusinessRuleResult> EnsureCanApplyMovementAsync(
        Guid medicationId,
        string movementType,
        int quantity,
        CancellationToken cancellationToken = default)
    {
        if (quantity == 0)
            return BusinessRuleResult.Failure("Inventory movement quantity cannot be zero");

        var normalizedMovementType = movementType.Trim();
        var medication = await _context.Medications
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.MedicationId == medicationId, cancellationToken);

        if (medication is null)
            return BusinessRuleResult.Failure("Medication does not exist");

        var currentStock = medication.Stock.GetValueOrDefault();
        var isStockExit = normalizedMovementType.Equals("Exit", StringComparison.OrdinalIgnoreCase) ||
            normalizedMovementType.Equals("Dispense", StringComparison.OrdinalIgnoreCase);

        if (isStockExit && quantity > currentStock)
            return BusinessRuleResult.Failure("Inventory movement exceeds available medication stock");

        return BusinessRuleResult.Success();
    }
}
