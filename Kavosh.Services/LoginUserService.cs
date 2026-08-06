using Kavosh.DataAccess.Repositories;
using Kavosh.Domain.Entities;
using Kavosh.Domain.Interfaces;
using Kavosh.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kavosh.Services
{
    public class LoginUserService
    {
        //private readonly ILoginUserRepository _loginUserRepository;
        //private readonly IRepository<LoginUser> _repository;
        private readonly ILoginUserRepository _loginUserRepository;

        public LoginUserService(ILoginUserRepository loginUserRepository)
        {
         
            _loginUserRepository = loginUserRepository;
        }

        public async Task<Dictionary<bool, string>> Enter(LoginUserDto dto)
        {
          Dictionary<bool, string> getStatus;
          getStatus = await _loginUserRepository.Enter(dto.Username, dto.Password);
          return getStatus;
        }

        public async Task<bool> UpdateAsync(LoginUserDto dto)
        {
            //_loginUserRepository.e
            var (entity, isNew) = await _loginUserRepository.GetOrNew(f => f.Username.ToLower() == dto.Username.ToLower());

            //await ValidateAsync(dto, isNew);

            entity.FullName = dto.FullName;
            entity.Username = dto.Username;
            entity.Password = dto.Password;


            if (isNew)
                await _loginUserRepository.Add(entity);
            else
                await _loginUserRepository.Update(entity);

            await _loginUserRepository.SaveChangesAsync();
            return isNew;
        }

        private static LoginUserDto ToDto(LoginUser p) => new()
        {
            Username = p.Username,
            Password = p.Password,
            FullName = p.FullName
        };
    }
}
