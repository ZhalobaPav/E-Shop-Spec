using ApplicationCore.Enities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class ProductFilterPaginatedSpecification:Specification<Product>
    {
        public ProductFilterPaginatedSpecification(int skip, int take, int? brandId, int? categoryId)
        {
            if(take == 0)
            {
                take = int.MaxValue;
            }
            Query
                .Where(i=>(!brandId.HasValue||brandId == i.BrandId)
                &&(!categoryId.HasValue||categoryId == i.CategoryId))
                .Skip(skip).Take(take);
        }
    }
}
