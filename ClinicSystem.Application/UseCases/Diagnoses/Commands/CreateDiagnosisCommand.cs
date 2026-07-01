using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Diagnoses.Commands;

public class CreateDiagnosisCommand : IRequest<Result<DiagnosisDto>>
{
   public  Guid MedicalRecordId { get; set; }
   public string Cie10Code {get; set;} = string.Empty;
    public string? Description {get; set;}
    public bool? IsPrimary {get; set;}
    public DateTime? NotedAt {get; set;}
}

public class CreateDiagnosisCommandHandler
    : IRequestHandler<CreateDiagnosisCommand, Result<DiagnosisDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateDiagnosisCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DiagnosisDto>> Handle(
        CreateDiagnosisCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new Diagnosis
        {
            DiagnosisId = Guid.NewGuid(),
            MedicalRecordId = request.MedicalRecordId,
            Cie10Code = request.Cie10Code,
            Description = request.Description,
            IsPrimary = request.IsPrimary,
            NotedAt = request.NotedAt
        };

        await _unitOfWork.Diagnoses.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<DiagnosisDto>.Success(entity.ToDto());
    }
}
//USAR ESTE ARCHIVO PARA TENER LA BASE PARA TENER DOMINIO DE GETTERS Y SETTERS 

