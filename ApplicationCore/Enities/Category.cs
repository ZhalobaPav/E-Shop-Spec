using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Enities
{
    public class Category:BaseEntity, IAggreagateRoot
    {
        public string Title { get; private set; }
        private Category()
        {
            
        }
        public Category(string title)
        {
            Title = title;
        }
    }
}
