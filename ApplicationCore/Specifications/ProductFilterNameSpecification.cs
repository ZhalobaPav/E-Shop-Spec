using ApplicationCore.Enities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class ProductFilterNameSpecification:Specification<Product>
    {
        public ProductFilterNameSpecification(string productName)
        {
            Query.Where(item => item.Name.Contains(productName));
        }
    }
}
