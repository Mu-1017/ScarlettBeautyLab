﻿using System;
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

        public async Task<(bool Succeeded, string ErrorMessage)> CreateUserAsync(RegisterForm form)
        {
            var entity = new UserEntity
            {
                Email = form.Email,
                UserName = form.Email,
                Nickname = form.Nickname,
                AgeGroup = form.AgeGroup,
                SkinType = form.SkinType
            };

            var result = await _userManager.CreateAsync(entity, form.Password);
            if (!result.Succeeded)
            {
                var firstError = result.Errors.FirstOrDefault()?.Description;
                return (false, firstError);
            }

            return (true, null);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            IQueryable<UserEntity> query = _userManager.Users;

            var items = await query.ProjectTo<User>(_mappingConfiguration)
                .ToListAsync();

            return items;

        }

        public async Task<User> GetUserAsync(ClaimsPrincipal user)
        {
            var entity = await _userManager.GetUserAsync(user);
            var mapper = _mappingConfiguration.CreateMapper();

            return mapper.Map<User>(entity);
        }

        public Task<User> GetUserByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Guid?> GetUserIdAsync(ClaimsPrincipal principal)
        {
            throw new NotImplementedException();
        }
    }
}
