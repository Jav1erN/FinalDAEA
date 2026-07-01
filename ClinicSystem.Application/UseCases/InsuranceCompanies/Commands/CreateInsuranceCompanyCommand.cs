using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.InsuranceCompanies.Commands;

public class CreateInsuranceCompanyCommand : IRequest<Result<InsuranceCompanyDto>>
{
    public string Name { get; set; } = string.Empty;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? ContactName { get; set; }

    public bool? IsActive { get; set; }
}

public class CreateInsuranceCompanyCommandHandler
    : IRequestHandler<CreateInsuranceCompanyCommand, Result<InsuranceCompanyDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateInsuranceCompanyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<InsuranceCompanyDto>> Handle(
        CreateInsuranceCompanyCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new InsuranceCompany
        {
            InsuranceCompanyId = Guid.NewGuid(),
            Name = request.Name,
            Phone = request.Phone,
            Email = request.Email,
            Address = request.Address,
            ContactName = request.ContactName,
            IsActive = request.IsActive
        };

        await _unitOfWork.InsuranceCompanies.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<InsuranceCompanyDto>.Success(entity.ToDto());
    }
}

