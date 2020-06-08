using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using BookApiLibary.Data;
using BookApiLibary.Models;
using BookApiLibary.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookApiLibary.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        public BookRepository Books { get; set; }

        public BookController(BookRepository books)
        {
            Books = books;
        
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult Get()
        {
            var token = Request.Headers[HeaderNames.Authorization];
            if (token.ToString() == null || token.ToString() == "") return null;

            var result = Books.ReadAll(token);
            return Ok(result);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var token = Request.Headers[HeaderNames.Authorization];
            if (token.ToString() == null || token.ToString() == "") return null;

            var result = Books.Read(id, token);
            return Ok(result);
        }

        // POST api/<controller>
        [HttpPut]
        public bool Put([FromBody]Book book)
        {
            var token = Request.Headers[HeaderNames.Authorization];
            if (token.ToString() == null || token.ToString() == "") return false;

           var result = Books.Update(book, token);
           return result;
        }

        // PUT api/<controller>
        [HttpPost()]
        public bool Post([FromBody]Book book)
        {
            var token = Request.Headers[HeaderNames.Authorization];
            if (token.ToString() == null || token.ToString() == "") return false;

            var result = Books.Create(book, token);
            return result;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var token = Request.Headers[HeaderNames.Authorization];
            if (token.ToString() == null || token.ToString() == "") return false;

            var result = Books.Delete(id, token);
            return result;
        }
    }
}
