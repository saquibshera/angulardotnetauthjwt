using System;
using Microsoft.EntityFrameworkCore;
using Myauth.Models;

namespace Myauth.Data
{
	public class DataContext:DbContext
	{
		public DataContext(DbContextOptions<DataContext>options):base(options)
		{
		}
        //dotnet ef migrations add initial
        //dotnet ef database update

        public DbSet<User> myusers { get; set; }
	}
}

