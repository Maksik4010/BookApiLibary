using BookApiLibary.Data;
using BookApiLibary.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiLibary.Repositories
{
    public class BookRepository
    {
        //Properites
        public ApplicationDbContext DbContext { get; set; }

        //Constructor
        public BookRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;

            if (dbContext.Books.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                dbContext.Books.Add(new Book { Title = "Item1", Author = "Jan", Year = 1234, Rent = true });
                dbContext.SaveChanges();
            }
        }

        //Methods
        public List<Book> ReadAll(string token)
        {
            var Tok = DbContext.Users.FirstOrDefault(o => o.Token == token);
            if (Tok == null) return null;

            return DbContext.Books.ToList();
        }

        public Book Read(Guid id, string token)
        {
            var Tok = DbContext.Users.FirstOrDefault(o => o.Token == token);
            if (Tok == null) return null;

            return DbContext.Books.FirstOrDefault(o => o.Id == id);
        }
        public bool Create (Book book, string token)
        {
            var Tok = DbContext.Users.FirstOrDefault(o => o.Token == token);
            if (Tok == null) return false;

            DbContext.Books.Add(book);
            DbContext.SaveChanges();
            return true;
        }

        public bool Update(Book book, string token)
        {
            var Tok = DbContext.Users.FirstOrDefault(o => o.Token == token);
            if (Tok == null) return false;

            DbContext.Books.Update(book);
            DbContext.SaveChanges();
            return true;
        }
        public bool Delete(Guid id, string token)
        {
            var Tok = DbContext.Users.FirstOrDefault(o => o.Token == token);
            if (Tok == null) return false;

            DbContext.Remove(new Book() { Id = id });
            DbContext.SaveChanges();
            return true;
        }   
    }
}
