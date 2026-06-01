using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.UseCases.LaboratoryResults.Dtos;

public static class LaboratoryResultMappings
{
    public static LaboratoryResultDto ToDto(this LaboratoryResult entity)
    {
        return new LaboratoryResultDto
        {
            ResultId = entity.ResultId,
            LaboratoryTestId = entity.LaboratoryTestId,
            ParameterName = entity.ParameterName,
            ResultValue = entity.ResultValue,
            Unit = entity.Unit,
            ReferenceRange = entity.ReferenceRange,
            IsAbnormal = entity.IsAbnormal,
            NotedAt = entity.NotedAt
        };
    }
}
