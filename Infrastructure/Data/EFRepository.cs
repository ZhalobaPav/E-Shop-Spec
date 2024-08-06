using ApplicationCore.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class EFRepository<T>:RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggreagateRoot
    {
        public EFRepository(ProdContext prodContext):base(prodContext)
        {
            
        }
    }
}
