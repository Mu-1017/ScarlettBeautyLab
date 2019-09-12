using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScarlettBeautyLab.Models;

namespace ScarlettBeautyLab.Services
{
    public class DefaultUserService : IUserService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfigurationProvider _mappingConfiguration;

        public DefaultUserService(UserManager<UserEntity> userManager, IConfigurationProvider mappingConfiguration)
        {
            _userManager = userManager;
            _mappingConfiguration = mappingConfiguration;
        }

        public Task<(bool Succeeded, string ErrorMessage)> CreateUserAsync(RegisterForm form)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserAsync(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Guid?> GetUserIdAsync(ClaimsPrincipal principal)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetUsersAsync()
        {
            IQueryable<UserEntity> query = _userManager.Users;

            var items = await query.ProjectTo<User>(_mappingConfiguration)
                .ToListAsync();

            return items;
            
        }
    }
}
