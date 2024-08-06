using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Enities
{
    public class Brand:BaseEntity, IAggreagateRoot
    {
        #pragma warning disable CS8618
        private Brand()
        {
            
        }
        public string Title { get; private set; }
        public Brand(string name)
        {
            Title = name;
        }
    }
}
