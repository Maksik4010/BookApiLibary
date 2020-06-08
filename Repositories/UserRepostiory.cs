using BookApiLibary.Data;
using BookApiLibary.Models;
using IdentityServer3.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookApiLibary.Repositories
{
    public class UserRepostiory
    {
        //Properites
        public ApplicationDbContext DbContext { get; set; }

        //Constructor
        public UserRepostiory(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;

            if (dbContext.Users.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                dbContext.Users.Add(new User { Username = "Maciej", Password = "1234!", Type = "admin", Status = "online" });
                dbContext.SaveChanges();
            }
        }

        //Methods
        public List<User> ReadAll(string token)
        {
            var Tok = DbContext.Users.FirstOrDefault(o => o.Token == token);
            if (Tok == null) return null;

            return DbContext.Users.ToList();
        }

        public User Read(Guid id, string token)
        {
            var Tok = DbContext.Users.FirstOrDefault(o => o.Token == token);
            if (Tok == null) return null;

            return DbContext.Users.FirstOrDefault(o => o.Id == id);
        }

        public string Generate()
        {
            string pass = GetUniqueKey(8);
            return pass;
        }
        public bool Create(User user, string token)
        {
            var Tok = DbContext.Users.FirstOrDefault(o => o.Token == token);
            if (Tok == null) return false;

            var find = DbContext.Users.FirstOrDefault(o => o.Username == user.Username);
            if (find != null) return false;
            DbContext.Users.Add(user);
            DbContext.SaveChanges();
            return true;
        }

        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetBytes(data);
            data = new byte[maxSize];
            crypto.GetBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
        public bool Update(User user, string token)
        {
            var Tok = DbContext.Users.FirstOrDefault(o => o.Token == token);
            if (Tok == null) return false;

            DbContext.Users.Update(user);
            DbContext.SaveChanges();
            return true;
        }
        public bool Delete(Guid id,string token)
        {
            var Tok = DbContext.Users.FirstOrDefault(o => o.Token == token);
            if (Tok == null) return false;

            DbContext.Remove(new User() { Id = id });
            DbContext.SaveChanges();
            return true;
        }
    }
}
