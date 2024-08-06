using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Enities.Buyer
{
    public class Buyer:BaseEntity, IAggreagateRoot
    {
        public string IdentityGuid { get;private set; }
        public List<PaymentMethod> _paymentMethods = new List<PaymentMethod>();
        public IEnumerable<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();
        private Buyer()
        {  
        }
        public Buyer(string identityGuid):this() 
        {
            Guard.Against.NullOrEmpty(identityGuid, nameof(identityGuid));
            IdentityGuid = identityGuid;
        }
    }
}
