using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiLibary.Models.JwtToken
{
    public class JwtToken
    {
        // Fields
        private JwtSecurityToken token;

        // Properties
        public DateTime ValidTo => token.ValidTo;
        public string Value => new JwtSecurityTokenHandler().WriteToken(token);

        // Constructor
        public JwtToken(JwtSecurityToken token)
        {
            this.token = token;
        }
    }
}
