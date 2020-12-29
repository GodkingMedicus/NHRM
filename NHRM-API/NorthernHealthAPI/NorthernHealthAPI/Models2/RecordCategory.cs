using System;
using System.Collections.Generic;

namespace NorthernHealthAPI.Models2
{
    public partial class RecordCategory
    {
        public RecordCategory()
        {
            RecordType = new HashSet<RecordType>();
        }

        public int RecordCategoryId { get; set; }
        public string Category { get; set; }

        public virtual ICollection<RecordType> RecordType { get; set; }
    }
}
