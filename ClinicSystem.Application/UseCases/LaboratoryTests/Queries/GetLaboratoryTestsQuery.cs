using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Application.UseCases.LaboratoryTests.Dtos;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.LaboratoryTests.Queries;

public record GetLaboratoryTestsQuery : IRequest<Result<IEnumerable<LaboratoryTestDto>>>;

public class GetLaboratoryTestsQueryHandler
    : IRequestHandler<GetLaboratoryTestsQuery, Result<IEnumerable<LaboratoryTestDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetLaboratoryTestsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<LaboratoryTestDto>>> Handle(
        GetLaboratoryTestsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Repository<LaboratoryTest>()
            .ListAsync(cancellationToken);

        return Result<IEnumerable<LaboratoryTestDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}
