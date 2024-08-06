using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Enities
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; protected set; }
        public DateTime CreatedDateUtc { get; set; } = DateTime.UtcNow;
    }
}
