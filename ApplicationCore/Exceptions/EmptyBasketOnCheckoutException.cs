using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Exceptions
{
    public class EmptyBasketOnCheckoutException:Exception
    {
        public EmptyBasketOnCheckoutException() : base("Basket cannot have 0 items on checkout")
        {
            
        }
    }
}
