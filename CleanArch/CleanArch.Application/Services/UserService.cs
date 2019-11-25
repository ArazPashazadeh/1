using System;
using CleanArch.Application.Interfaces;
using CleanArch.Application.Security;
using CleanArch.Application.ViewModels.User;
using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;

namespace CleanArch.Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public CheckUser CheckUser(string userName, string email)
        {
            bool userNameValid = _userRepository.IsExistUserName(userName);
            bool emailValid = _userRepository.IsExistEmail(email.Trim().ToLower());

            if (userNameValid && emailValid)
                return ViewModels.User.CheckUser.UserNameAndEmailNotValid;
            else if (emailValid)
                return ViewModels.User.CheckUser.EmailNotValid;
            else if (userNameValid)
                return ViewModels.User.CheckUser.UserNameNotValid;
            return ViewModels.User.CheckUser.Ok;
        }

        public Guid RegisterUser(User user)
        {
            _userRepository.AddUser(user);
            _userRepository.Save();
            return user.Id;
        }

        public bool IsExistUser(string email, string password)
        {
            return _userRepository.IsExistUser(email.Trim().ToLower(), PasswordHelper.EncodePasswordMd5(password));
        }
    }
}
