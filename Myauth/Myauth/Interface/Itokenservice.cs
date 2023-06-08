using System;
using Myauth.Models;

namespace Myauth.Interface
{
	public interface Itokenservice
	{
		
		string Createtoken(User user);
	}
}

