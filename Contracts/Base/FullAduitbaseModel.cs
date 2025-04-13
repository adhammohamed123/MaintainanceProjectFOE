using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Contracts.Base
{
   
    public abstract class SoftDeletedIdentityModel:IIdentityModel,ISoftDeletedModel
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
    }

}
