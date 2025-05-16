using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FZC.Domain.Entities
{
    public abstract class BaseEntity
    {
        public DateTime updateDate { get; set; }
        public DateTime createDate { get; set; }
        public bool isDeleted { get; set; }
    }
}
