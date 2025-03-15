namespace Contracts.Base
{
    public interface ISoftDeletedModel {
        public bool IsDeleted { get; set; }
    }
}
