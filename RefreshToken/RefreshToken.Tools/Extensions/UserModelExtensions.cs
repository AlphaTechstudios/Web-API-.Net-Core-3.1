using RefreshToken.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RefreshToken.Tools.Extensions
{
    public static class UserModelExtensions
    {
        public static IEnumerable<UserModel> WithoutPassword(this IEnumerable<UserModel> userModels) => userModels.Select(x => x.WithoutPassword());

        public static UserModel WithoutPassword(this UserModel userModel)
        {
            if (userModel == null)
            {
                return null;
            }

            userModel.Password = string.Empty;
            return userModel;
        }
    }
}
