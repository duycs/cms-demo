using System;
using System.Collections.Generic;

namespace cms_demo.Models
{
    public partial class ObjectFileds
    {
        public int Id { get; set; }
        public string ObjectId { get; set; }
        public int? FieldId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public byte? IsDeleted { get; set; }
    }
}
