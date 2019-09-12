using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScarlettBeautyLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScarlettBeautyLab
{
    public class BeautyLabDbContext : IdentityDbContext<UserEntity, UserRoleEntity, Guid>
    {
        public BeautyLabDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
