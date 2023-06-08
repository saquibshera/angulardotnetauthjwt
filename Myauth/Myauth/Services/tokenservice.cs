using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Myauth.Interface;
using Myauth.Models;

namespace Myauth.Services
{
    public class tokenservice : Itokenservice
    {
        private readonly SymmetricSecurityKey _key;

        public tokenservice(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string Createtoken(User user)
        {
            var claims = new List<Claim>
          {
              new Claim(JwtRegisteredClaimNames.NameId,user.Username)
          };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

            var tokendescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            var tokenhandler = new JwtSecurityTokenHandler();

            var token = tokenhandler.CreateToken(tokendescriptor);

            return tokenhandler.WriteToken(token);

        }
    }
}

