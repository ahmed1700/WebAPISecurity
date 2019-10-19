using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPToken.Models;
using WebAPToken.Services;

namespace WebAPToken.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserServices _userServices;
        public UsersController(IUserServices userServices)
        {
            this._userServices = userServices;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User userModel)
        {
            var user = _userServices.Authenticate(userModel.UserName, userModel.Password);
            if (user == null) return BadRequest(new { Message = "UserName or Password Invalid" });
            return Ok(user);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            var users = _userServices.GetAllUsers();
            return Ok(users);
        }
    }
}
