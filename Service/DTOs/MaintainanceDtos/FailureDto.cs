using Core.Entities.Enums;

namespace Service.DTOs.MaintainanceDtos
{
    public record FailureDto(int id, string Name, FailureActionDone State);
}
