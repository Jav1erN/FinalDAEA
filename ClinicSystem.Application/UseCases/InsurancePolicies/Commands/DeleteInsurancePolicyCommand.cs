using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.InsurancePolicies.Commands;

public class DeleteInsurancePolicyCommand : IRequest<Result<bool>>
{
    public Guid InsurancePolicyId { get; set; } = Guid.Empty;
}

public class DeleteInsurancePolicyCommandHandler
    : IRequestHandler<DeleteInsurancePolicyCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteInsurancePolicyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteInsurancePolicyCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.InsurancePolicies;
        var entity = await repository.GetByIdAsync(request.InsurancePolicyId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("InsurancePolicy not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}

