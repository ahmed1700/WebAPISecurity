using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPToken.Helpers;
using WebAPToken.Models;

namespace WebAPToken.Services
{
    public class UserServcies : IUserServices
    {
        private readonly AppSettings _appSettings;
        public UserServcies(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public List<User> _users = new List<User>
        {
            new User
            {
                Id=1 , FirstName="demo" , LastName="demo", UserName="demo", Password="demo" , Email="demo@demo.com"
            },
             new User
            {
                Id=2 , FirstName="test" , LastName="test", UserName="test",  Password="test" , Email="test@test.com"
            },
        };
        public User Authenticate(string userName, string Password)
        {
            // 1 : Check if user exist = username and Password Correct
            var user = _users.SingleOrDefault(u => u.UserName == userName && u.Password == Password);
            if (user==null)
            {
                return null;
            }
            // 2 : Authentication : Succssed : Prepare to Generate Token
            //designed for creating and validating Json Web Tokens. 
            var tokenHandler = new JwtSecurityTokenHandler();
            // Read Secrete key from App Setting
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(20),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                SigningCredentials= new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = null;
            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users.Select(u =>
            {
                u.Password = null;
                return u;
            });
        }
    }
}
