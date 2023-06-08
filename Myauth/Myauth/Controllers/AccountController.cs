using System.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Myauth.Data;
using Myauth.dtos;
using Myauth.Models;
using Myauth.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Myauth.Controllers
{
    public class AccountController : BaseController
    {
        private DataContext _context;
        private readonly Itokenservice _tokenservice;
        public AccountController(DataContext context,Itokenservice tokenservice)
        {
            _tokenservice = tokenservice;
            _context = context;
        }
        [HttpPost("Register")]
        public ActionResult<Userdto> register([FromBody] Register rto)    //register
        {
            var x = _context.myusers.Any(v => v.Username == rto.Username);
            if(x)
            {
                return BadRequest("username exists");
                
               
            }
            using var hmac = new HMACSHA512();

            var user = new User
            {
                Username =rto.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(rto.Password)),
                PasswordSalt = hmac.Key
            };
            _context.myusers.Add(user);
            _context.SaveChanges();
            return new Userdto{
                Username=user.Username,
                Token=_tokenservice.Createtoken(user)
            };
            //_mapper.map<userdto,user>();
        }
        [Authorize]
        [HttpGet("GetUsers")]                         //authenticate
        public List<User> getusers()
        {
            return _context.myusers.ToList();
        }

        [HttpPost("login")]
        public ActionResult<Userdto> Login([FromBody]login lto)  //login action
        {
            var user = _context.myusers.SingleOrDefault(x => x.Username == lto.Username);
            if(user==null)
            {
                return Unauthorized("no user exists");
            }
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computehash = hmac.ComputeHash(Encoding.UTF8.GetBytes(lto.Password));
            for(int i=0;i<computehash.Length;i++)
            {
                if (computehash[i] != user.PasswordHash[i])

                    return Unauthorized("Invalid password");
            }
            return new Userdto{
                Username=user.Username,
                Token=_tokenservice.Createtoken(user)
            };
        }
       
    }
}

