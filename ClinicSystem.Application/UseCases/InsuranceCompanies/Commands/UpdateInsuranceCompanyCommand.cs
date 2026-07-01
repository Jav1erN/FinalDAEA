using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.InsuranceCompanies.Commands;

public class UpdateInsuranceCompanyCommand : IRequest<Result<InsuranceCompanyDto>>
{
    public Guid InsuranceCompanyId { get; set; } = Guid.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? ContactName { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

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
        var repository = _unitOfWork.InsuranceCompanies;
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

