using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Specialties.Commands;

public record DeleteSpecialtyCommand(Guid SpecialtyId) : IRequest<Result<bool>>;

public class DeleteSpecialtyCommandHandler
    : IRequestHandler<DeleteSpecialtyCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSpecialtyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteSpecialtyCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Specialties;
        var entity = await repository.GetByIdAsync(request.SpecialtyId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("Specialty not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}

