using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using BookApiLibary.Models;
using BookApiLibary.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookApiLibary.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public UserRepostiory Users { get; set; }

        public UserController(UserRepostiory users)
        {
            Users = users;
        }


        // GET: api/<controller>
        [HttpGet]
        public ActionResult Get()
        {
            var token = Request.Headers[HeaderNames.Authorization];
            if (token.ToString() == null || token.ToString() == "" ) return null; 

            var result = Users.ReadAll(token);
            return Ok(result);
        }

        [HttpGet("Generate")]
        public ActionResult GetPassword()
        {
            var result = Users.Generate();
            return Ok(result);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var token = Request.Headers[HeaderNames.Authorization];
            if (token.ToString() == null || token.ToString() == "") return null;

            var result = Users.Read(id, token);
            return Ok(result);
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromBody]User user)
        {
            var token = Request.Headers[HeaderNames.Authorization];
            if (token.ToString() == null || token.ToString() == "") return null;
           
            Users.Update(user, token);
            return Ok();
        }

        // PUT api/<controller>
        [HttpPut()]
        public bool Put([FromBody]User user)
        {
            var token = Request.Headers[HeaderNames.Authorization];
            if (token.ToString() == null || token.ToString() == "") return false;
            
            var result = Users.Create(user, token);
            return result;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var token = Request.Headers[HeaderNames.Authorization];
            if (token.ToString() == null || token.ToString() == "") return null;
            
            var result = Users.Delete(id, token);
            return Ok(result);
        }
    }
}