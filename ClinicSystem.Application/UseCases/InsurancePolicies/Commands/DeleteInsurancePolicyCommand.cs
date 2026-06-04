using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.InsurancePolicies.Commands;

public record DeleteInsurancePolicyCommand(Guid InsurancePolicyId) : IRequest<Result<bool>>;

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
        var repository = _unitOfWork.Repository<InsurancePolicy>();
        var entity = await repository.GetByIdAsync(request.InsurancePolicyId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("InsurancePolicy not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
