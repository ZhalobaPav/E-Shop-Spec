using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class UserService
    {
        private readonly AppIdentityDbContext dbContext;
        private readonly DbSet<ApplicationUser> users;

        public UserService(AppIdentityDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.users = dbContext.Set<ApplicationUser>();
        }
        public async Task<List<ApplicationUser>> GetUsers()
        {
            return await users.ToListAsync();
        }
    }
}
