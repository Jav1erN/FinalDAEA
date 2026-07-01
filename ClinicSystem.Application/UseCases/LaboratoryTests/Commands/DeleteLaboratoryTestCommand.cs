using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.LaboratoryTests.Commands;

public class DeleteLaboratoryTestCommand : IRequest<Result<bool>>
{
    public Guid LaboratoryTestId { get; set; } = Guid.Empty;
}

public class DeleteLaboratoryTestCommandHandler
    : IRequestHandler<DeleteLaboratoryTestCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLaboratoryTestCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteLaboratoryTestCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.LaboratoryTests;
        var entity = await repository.GetByIdAsync(request.LaboratoryTestId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("LaboratoryTest not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}

