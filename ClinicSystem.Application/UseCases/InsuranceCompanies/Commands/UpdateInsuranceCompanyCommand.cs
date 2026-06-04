using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.InsuranceCompanies.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.InsuranceCompanies.Commands;

public record UpdateInsuranceCompanyCommand(
    Guid InsuranceCompanyId,
    string Name,
    string? Phone,
    string? Email,
    string? Address,
    string? ContactName,
    bool? IsActive,
    DateTime? UpdatedAt
) : IRequest<Result<InsuranceCompanyDto>>;

public class UpdateInsuranceCompanyCommandHandler
    : IRequestHandler<UpdateInsuranceCompanyCommand, Result<InsuranceCompanyDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateInsuranceCompanyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<InsuranceCompanyDto>> Handle(
        UpdateInsuranceCompanyCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<InsuranceCompany>();
        var entity = await repository.GetByIdAsync(request.InsuranceCompanyId, cancellationToken);

        if (entity is null)
            return Result<InsuranceCompanyDto>.Failure("InsuranceCompany not found");

        entity.Name = request.Name;
        entity.Phone = request.Phone;
        entity.Email = request.Email;
        entity.Address = request.Address;
        entity.ContactName = request.ContactName;
        entity.IsActive = request.IsActive;
        entity.UpdatedAt = request.UpdatedAt;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<InsuranceCompanyDto>.Success(entity.ToDto());
    }
}
