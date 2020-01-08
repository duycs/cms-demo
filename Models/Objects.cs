using System;
using System.Collections.Generic;

namespace cms_demo.Models
{
    public partial class Objects
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? PermissionId { get; set; }
        public int? ObjectTypeId { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public byte? IsDeleted { get; set; }
    }
}
