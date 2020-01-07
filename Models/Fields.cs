using System;
using System.Collections.Generic;

namespace cms_demo.Models
{
    public partial class Fields
    {
        public int Id { get; set; }
        public string AccessModifier { get; set; }
        public int? FieldTypeId { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public byte? IsDeleted { get; set; }
    }
}
