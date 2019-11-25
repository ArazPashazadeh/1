using System;
using CleanArch.Application.ViewModels.User;
using CleanArch.Domain.Models;

namespace CleanArch.Application.Interfaces
{
    public interface IUserService
    {
        CheckUser CheckUser(string userName, string email);
        Guid RegisterUser(User user);
        bool IsExistUser(string email, string password);
    }
}
