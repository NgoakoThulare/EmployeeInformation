using Model.User;
using static Model.Enums;

namespace EmployeeInformation.Services
{
    /// <summary>
    /// Employee service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticates user.
        /// </summary>
        /// <param name="username">Registered username.</param>
        /// <param name="password">Registered password.</param>
        /// <returns></returns>
        User Authenticate(string username, string password);

        /// <summary>
        /// Authenticates user.
        /// </summary>
        /// <param name="username">Required username.</param>
        /// <param name="password">Required password.</param>
        /// <param name="role">Required password.</param>
        /// <returns></returns>
        bool Register(string username, string password, Role role);
    }
}
