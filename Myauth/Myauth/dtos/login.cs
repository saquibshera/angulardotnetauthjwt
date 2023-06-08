using System;
using System.ComponentModel.DataAnnotations;

namespace Myauth.dtos
{
	public class login
	{
        [Required]
        public string Username { get; set; }


        [Required]
        public string Password { get; set; }
    }
}

