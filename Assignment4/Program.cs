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
        static void Main(string[] args)
        {
            //var connectionString = "Server=localhost;Database=Kanban;User Id=sa;Password=$connectionString = "Server=localhost;Database=$database;User Id=sa;Password=$password"";
            //docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
            var configuration = LoadConfiguration();
            var connectionString = configuration.GetConnectionString("Assignment4");
            

            var optionsBuilder = new DbContextOptionsBuilder<KanbanContext>().UseSqlServer(connectionString);
            using var context = new KanbanContext(optionsBuilder.Options);
            
            //dotnet user-secrets set "ConnectionStrings:Assignment4" "$connectionString"
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
