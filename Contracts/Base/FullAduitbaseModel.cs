using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Contracts.Base
{
   
    public abstract class SoftDeletedIdentityModel:IIdentityModel,ISoftDeletedModel
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
    }

}
