using System;
using System.IO;
using Assignment4.Core;
using Assignment4.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Assignment4.Entities.Tests
{
    public class UserRepositoryTests : IDisposable
    {
        private readonly KanbanContext _context;
        private readonly UserRepository _repo;

        public UserRepositoryTests()
        {
            /*var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<ComicsContext>();
            builder.UseSqlite(connection);
            var context = new ComicsContext(builder.Options);
            context.Database.EnsureCreated();
            context.Cities.Add(new User { Name = "Metropolis" });
            context.SaveChanges();

            _context = context;
            _repo = new UserRepository(_context);*/



            //var connectionString = "Server=localhost;Database=Kanban;User Id=sa;Password=$connectionString = "Server=localhost;Database=Kanban;User Id=sa;Password=640042d4-f367-45a2-b412-84117a5e1d88"";
            //docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
            var configuration =  new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Assignment4/appsettings.json")
                .AddUserSecrets<User>().Build(); //not user
            
            
            var connectionString = configuration.GetConnectionString("Kanban");
            

            var optionsBuilder = new DbContextOptionsBuilder<KanbanContext>().UseSqlServer(connectionString);
            using var context = new KanbanContext(optionsBuilder.Options);
            
            //dotnet user-secrets set "ConnectionStrings:Assignment4" "$connectionString"

            KanbanContextFactory.Seed(context);
            _context = context;
            _repo = new UserRepository(_context);
        }

        [Fact]
        public void Create_given_user_returns_user_with_Id()
        {
            var user = new UserCreateDTO
            {
                Name = "Harry Potter", 
                Email = "HP.diagonal.com"
            };
            
            var created = _repo.Create(user);

            Assert.Equal(Response.Created, created.Response);
        }

        [Fact]
        public void Read_given_non_existing_id_returns_null()
        {
            var user = _repo.Read(42);

            Assert.Null(user);
        }

        [Fact]
        public void Read_given_existing_id_returns_user()
        {
            var user = _repo.Read(1);

            Assert.Equal(new UserDTO(1, "Harry Potter", "HP.diagonal.com"), user);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}