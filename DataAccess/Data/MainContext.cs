using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Data
{
    public class MainContext : DbContext
    {

        private string ConnectionString;

        public MainContext(DbContextOptions options) : base(options)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional : false);
            var configuration = builder.Build();
            ConnectionString = configuration.GetConnectionString("Default");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
