using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using static Model.Enums;

namespace EmployeeInformation.Services
{
    /// <summary>
    /// User service.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        /// <summary>
        /// Configuration settings object.
        /// </summary>
        /// <param name="appSettings"></param>
        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        /// <summary>
        /// Authenticates user.
        /// </summary>
        /// <param name="username">Registered username.</param>
        /// <param name="password">Registered password.</param>
        /// <returns></returns>
        public User Authenticate(string username, string password)
        {
            string path = string.Concat(_appSettings.FilePath, _appSettings.UsersFile, ".json");
            var users = new List<User>();
            if (File.Exists(path))
            {
                string fileContents = File.ReadAllText(path);

                if (!string.IsNullOrWhiteSpace(fileContents))
                {
                    users = JsonConvert.DeserializeObject<List<User>>(fileContents);
                }
            }

            var user = users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // user not found
            if (user == null)
                return null;

            // if user was found generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, Enum.GetName(typeof(Role), user.Role))
                }),
                Expires = DateTime.UtcNow.AddMinutes(45),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = string.Empty;

            return user;
        }

        /// <summary>
        /// Registers users of the application.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool Register(string username, string password, Role role)
        {
            string path = string.Concat(_appSettings.FilePath, _appSettings.UsersFile, ".json");
            var users = new List<User>();

            //Register users
            try
            {
                if (!File.Exists(path))
                {
                    users.Add(new User
                    {
                        Id = 1,
                        Username = username,
                        Password = password,
                        Role = role
                    });

                    string contents = JsonConvert.SerializeObject(users);

                    File.WriteAllText(path, contents);
                    return true;
                }

                string fileContents = File.ReadAllText(path);

                if (!string.IsNullOrWhiteSpace(fileContents))
                {
                    var existingUsers = JsonConvert.DeserializeObject<List<User>>(fileContents);

                    int maxUserId = existingUsers.Max(x => x.Id);

                    var newUser = new User
                    {
                        Id = maxUserId + 1,
                        Username = username,
                        Password = password,
                        Role = role
                    };

                    existingUsers.Add(newUser);

                    string newContents = JsonConvert.SerializeObject(existingUsers);

                    File.WriteAllText(path, newContents);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }
    }
}
