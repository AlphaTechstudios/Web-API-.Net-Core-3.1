using Managers.Common;
using Managers.Interfaces;
using RefreshToken.Models.Dtos;
using RefreshToken.Models.Entities;
using RefreshToken.Models.Enums;
using RefreshToken.Tools.Extensions;
using Repositories.Common;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Managers
{
    public class UsersManager : BaseManager, IUsersManager
    {
        private readonly IUsersRepository usersRepository;

        public UsersManager(IUnitOfWork unitOfWork, IUsersRepository usersRepository)
            : base(unitOfWork)
        {
            this.usersRepository = usersRepository;
        }

        public void Delete(long id)
        {
            usersRepository.Delete(id);
            UnitOfWork.Commit();
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return usersRepository.Get(x => x.DeleteDate == null).WithoutPassword();
        }

        public UserModel GetUserByEmail(string email)
        {
            return usersRepository.Get(x => x.Email == email && x.DeleteDate == null).SingleOrDefault().WithoutPassword();
        }

        public long Insert(UserModel userModel)
        {
            userModel.CreationDate = userModel.ModificationDate = DateTime.UtcNow;
            userModel.Status = StatusEnum.Pending;
            usersRepository.Insert(userModel);
            UnitOfWork.Commit();
            return userModel.ID;

        }

        public UserModel Login(LoginDto loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                return null;
            }

            return usersRepository.Get(x => x.DeleteDate == null && x.Email == loginDto.Email && x.Password == loginDto.Password).SingleOrDefault().WithoutPassword();
        }

        public UserModel Update(UserModel userModel)
        {
            var targetUser = usersRepository.Get(x => x.DeleteDate == null && x.Email == userModel.Email).SingleOrDefault();

            if (targetUser != null)
            {
                userModel.ModificationDate = DateTime.UtcNow;
                userModel.Password = targetUser.Password;
                usersRepository.Update(userModel);
                UnitOfWork.Commit();
                return userModel.WithoutPassword();
            }
            return null;
        }
    }
}
