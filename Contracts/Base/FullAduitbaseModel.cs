namespace Contracts.Base
{
    public abstract class FullAduitbaseModel : IIdentityModel, IAuditedModel, ISoftDeletedModel
    {
        public string CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? LastModifiedUserId { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
    }
    public abstract class SoftDeletedIdentityModel:IIdentityModel,ISoftDeletedModel
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
    }

}
