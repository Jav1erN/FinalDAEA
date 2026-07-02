using ClinicSystem.Infrastructure.Adapters.Services;

namespace ClinicSystem.Tests;

public class BillingPolicyServiceTests
{
    [Fact]
    public void ValidateAmounts_ReturnsSuccess_WhenAmountsAreValid()
    {
        var service = new BillingPolicyService();

        var result = service.ValidateAmounts(100m, 10m, 20m);

        Assert.True(result.IsValid);
        Assert.Null(result.Error);
    }

    [Fact]
    public void ValidateAmounts_ReturnsFailure_WhenDiscountAndCoverageExceedSubtotal()
    {
        var service = new BillingPolicyService();

        var result = service.ValidateAmounts(100m, 60m, 50m);

        Assert.False(result.IsValid);
        Assert.Equal("Billing discounts and coverage cannot exceed subtotal", result.Error);
    }

    [Fact]
    public void CalculateTotal_SubtractsDiscountAndInsuranceCoverage()
    {
        var service = new BillingPolicyService();

        var total = service.CalculateTotal(250m, 25m, 50m);

        Assert.Equal(175m, total);
    }
}
