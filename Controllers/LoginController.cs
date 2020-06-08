using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using BookApiLibary.Models;
using BookApiLibary.Models.JwtToken;
using BookApiLibary.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookApiLibary.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        //public LoginRepository Logins { get; set; }
        public LoginRepository Users { get; set; }

        public LoginController(LoginRepository logins)
        {
            Users = logins;
        }

        // GET api/<controller>/5
        [HttpPost("Login")]
        public ActionResult Post([FromBody]User user)
        {
            //var username = user.Username;
            var result = Users.Login(user);
            var odp = user.Id;

           

            return Ok(result);
        }

        [HttpPost("Logout")]
        public ActionResult Postt([FromBody]User user)
        {
            var result = Users.Logout(user);
            return Ok(result);
        }
  

        
    }
}
