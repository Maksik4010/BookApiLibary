using BookApiLibary.Data;
using BookApiLibary.Models;
using BookApiLibary.Models.JwtToken;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace BookApiLibary.Repositories
{
    public class LoginRepository
    {
        //Properites
        public ApplicationDbContext DbContext { get; set; }

        //Constructor
        public LoginRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }


        //Methods
        public User Login(User user)
        {
            var users = DbContext.Users.FirstOrDefault(o => o.Username == user.Username);
            if (users != null)
            {
                var userpass = DbContext.Users.FirstOrDefault(o => o.Password == user.Password);
                if (userpass != null)
                {
                    var userss = DbContext.Users.First(o => o.Id == users.Id);

                    if (user.Token == null || user.Token == "")
                    {
                        var token = new JwtTokenBuilder()
                              .AddSecurityKey(JwtSecurityKEy.Create("fiver-secret-key"))
                              .AddSubject("james bond")
                              .AddIssuer("Fiver.Security.Bearer")
                              .AddAudience("Fiver.Security.Bearer")
                              .AddClaim("MembershipId", "111")
                              .AddExpiry(1)
                              .Build();

                        userss.Token = token.Value;
                    }
                    else userss.Token = user.Token;

                    var a = users.Id;
                    var b = users.Type;                  
                    userss.Password = user.Password;                
                    userss.Status = "online";
                    userss.Type = b;
                    DbContext.SaveChanges();
                    DbContext.Users.Update(userss);
                    DbContext.SaveChanges();
                    return userss;
                }
            }
            return null;
        }

        public bool Logout (User user)
        {
            var users = DbContext.Users.FirstOrDefault(o => o.Username == user.Username);
            if (users == null) return false;

            var userss = DbContext.Users.First(o => o.Id == users.Id);

            var a = users.Id;
            var b = users.Type;

            userss.Token = null;
            userss.Password = users.Password;
            userss.Status = "offline";
            userss.Type = b;
            DbContext.SaveChanges();
            DbContext.Users.Update(userss);
            DbContext.SaveChanges();
            return true;
        }

        public void Update(User user)
        {
            DbContext.Users.Update(user);
            DbContext.SaveChanges();
        }
    }
}
