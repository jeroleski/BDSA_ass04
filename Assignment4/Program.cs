using System;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Assignment4.Entities;

namespace Assignment4
{
    class Program
    {
        //public static KanbanContext _context {private set; get;}

        static void Main(string[] args)
        {
            //var connectionString = "Server=localhost;Database=Kanban;User Id=sa;Password=$connectionString = Server=localhost;Database=Kanban;User Id=sa;Password=640042d4-f367-45a2-b412-84117a5e1d88";
            //docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
            var configuration = LoadConfiguration();
            var connectionString = configuration.GetConnectionString("Kanban");
            var optionsBuilder = new DbContextOptionsBuilder<KanbanContext>().UseSqlServer(connectionString);
            using var context = new KanbanContext(optionsBuilder.Options);
            KanbanContextFactory.Seed(context);
            //_context = context;
                        
        }

        static IConfiguration LoadConfiguration()
{
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Assignment4/appsettings.json")
                .AddUserSecrets<Program>();

            return builder.Build();
        }
    }
}
