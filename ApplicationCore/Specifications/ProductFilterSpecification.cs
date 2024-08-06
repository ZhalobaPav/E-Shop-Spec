using ApplicationCore.Enities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class ProductFilterSpecification:Specification<Product>
    {
        public ProductFilterSpecification(int? brandId, int? categoryId)
        {
            Query.Where(i => (!brandId.HasValue || i.BrandId == brandId) 
                && (!categoryId.HasValue || i.CategoryId == categoryId));
        }
    }
}
