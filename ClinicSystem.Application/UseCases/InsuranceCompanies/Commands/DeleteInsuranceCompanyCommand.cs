using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.InsuranceCompanies.Commands;

public record DeleteInsuranceCompanyCommand(Guid InsuranceCompanyId) : IRequest<Result<bool>>;

public class DeleteInsuranceCompanyCommandHandler
    : IRequestHandler<DeleteInsuranceCompanyCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteInsuranceCompanyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteInsuranceCompanyCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<InsuranceCompany>();
        var entity = await repository.GetByIdAsync(request.InsuranceCompanyId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("InsuranceCompany not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
