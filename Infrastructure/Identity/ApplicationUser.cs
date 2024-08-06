using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class ApplicationUser:IdentityUser, IAggreagateRoot
    {
        public UserType UserType { get; set; }
        public string Password { get; set; }
    }
}
