using System;
using System.Collections.Generic;

namespace cms_demo.Models
{
    public partial class FieldTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public byte? IsDeleted { get; set; }
    }
}
