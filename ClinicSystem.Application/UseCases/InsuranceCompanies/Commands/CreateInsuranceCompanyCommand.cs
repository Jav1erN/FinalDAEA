using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.InsuranceCompanies.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.InsuranceCompanies.Commands;

public record CreateInsuranceCompanyCommand(
    string Name,
    string? Phone,
    string? Email,
    string? Address,
    string? ContactName,
    bool? IsActive
) : IRequest<Result<InsuranceCompanyDto>>;

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

        await _unitOfWork.Repository<InsuranceCompany>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<InsuranceCompanyDto>.Success(entity.ToDto());
    }
}
