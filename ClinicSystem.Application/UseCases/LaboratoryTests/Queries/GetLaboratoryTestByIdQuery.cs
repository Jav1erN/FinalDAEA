using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.LaboratoryTests.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.LaboratoryTests.Queries;

public record GetLaboratoryTestByIdQuery(Guid LaboratoryTestId) : IRequest<Result<LaboratoryTestDto>>;

public class GetLaboratoryTestByIdQueryHandler
    : IRequestHandler<GetLaboratoryTestByIdQuery, Result<LaboratoryTestDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetLaboratoryTestByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<LaboratoryTestDto>> Handle(
        GetLaboratoryTestByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.LaboratoryTests
            .GetByIdAsync(request.LaboratoryTestId, cancellationToken);

        if (entity is null)
            return Result<LaboratoryTestDto>.Failure("LaboratoryTest not found");

        return Result<LaboratoryTestDto>.Success(entity.ToDto());
    }
}

