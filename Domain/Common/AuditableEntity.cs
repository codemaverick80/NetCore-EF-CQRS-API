using System;

namespace Domain.Common
{
    public class AuditableEntity: EntityBase
    {
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }   

    }
}
