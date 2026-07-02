namespace ClinicSystem.Domain.Common;

public sealed class BusinessRuleResult
{
    private BusinessRuleResult(bool isValid, string? error)
    {
        IsValid = isValid;
        Error = error;
    }

    public bool IsValid { get; }

    public string? Error { get; }

    public static BusinessRuleResult Success()
    {
        return new BusinessRuleResult(true, null);
    }

    public static BusinessRuleResult Failure(string error)
    {
        return new BusinessRuleResult(false, error);
    }
}
