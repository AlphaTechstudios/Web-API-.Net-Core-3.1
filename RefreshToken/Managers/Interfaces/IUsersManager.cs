using RefreshToken.Models.Dtos;
using RefreshToken.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Managers.Interfaces
{
    public interface IUsersManager
    {

        IEnumerable<UserModel> GetAllUsers();
        UserModel Login(LoginDto loginDto);

        long Insert(UserModel userModel);

        UserModel Update(UserModel userModel);

        void Delete(long id);

        UserModel GetUserByEmail(string email);

    }
}
