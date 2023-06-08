using System;
using System.ComponentModel.DataAnnotations;

namespace Myauth.Models
{
	public class Register
	{
		[Required]
		public string Username { get; set; }


		[Required]
		public string Password { get; set; }
	}
}

