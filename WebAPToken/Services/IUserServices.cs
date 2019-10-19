using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPToken.Models;

namespace WebAPToken.Services
{
    public interface IUserServices
    {
        User Authenticate(string userName, string Password);

        IEnumerable<User> GetAllUsers();
    }
}
