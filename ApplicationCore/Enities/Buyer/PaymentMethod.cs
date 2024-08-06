using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Enities.Buyer
{
    public class PaymentMethod:BaseEntity
    {
        public string? Allias { get; private set; }
        public string? Card { get; private set; }
        public string? Last4 { get; private set; }
    }
}
