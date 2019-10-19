using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPToken.Models;
using WebAPToken.Services;

namespace WebAPToken.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
   
        private IUserServices _userServices;
        public ValuesController(IUserServices userServices)
        {
            this._userServices = userServices;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] User userModel)
        {
            var user = _userServices.Authenticate(userModel.UserName, userModel.Password);
            if (user == null) return BadRequest(new { Message = "UserName or Password Invalid" });
            return Ok(user);
        }
        public IActionResult GetAll()
        {
            var users = _userServices.GetAllUsers();
            return Ok(users);
        }
    }
}
