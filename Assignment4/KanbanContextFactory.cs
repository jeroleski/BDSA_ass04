using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Assignment4.Entities;
using Assignment4.Core;
using System.Collections.Generic;


namespace Assignment4
{
    public class KanbanContextFactory : IDesignTimeDbContextFactory<KanbanContext>
        {
        public KanbanContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<Program>()
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("Kanban");

            var optionsBuilder = new DbContextOptionsBuilder<KanbanContext>()
                .UseSqlServer(connectionString);

            return new KanbanContext(optionsBuilder.Options);
        }

        public static void Seed(KanbanContext context)
        {
            context.Database.ExecuteSqlRaw("DELETE dbo.Tags");
            context.Database.ExecuteSqlRaw("DELETE dbo.Tasks");
            context.Database.ExecuteSqlRaw("DELETE dbo.Users");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Tags', RESEED, 0)");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Tasks', RESEED, 0)");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Users', RESEED, 0)");

            var user1 = new User { Name = "Harry Potter", Email = "HP.diagonal.com" };
            var user2 = new User { Name = "Ronald Weasley", Email = "Cannons4Life@quidditch.com" };
            var user3 = new User { Name = "Hermione Granger", Email = "ILoveBooks@MoM.com" };
            var user4 = new User { Name = "Gellert Grindelwald", Email = "fantastic@beasts.us" };
            var user5 = new User { Name = "Tom Marvolo Riddle", Email = "IAmVoldemort@slytherin.com" };
            var user6 = new User { Name = "Albus Dumbledore", Email = "FuglFonix@hogwarts.uk" };

            var tag1 = new Tag { Name = "Lumos" };
            var tag2 = new Tag { Name = "Sectumsempra" };
            var tag3 = new Tag { Name = "Wingardium Leviosai" };
            var tag4 = new Tag { Name = "Expecto Patronum" };
            var tag5 = new Tag { Name = "Avada Kedavra" };
            var tag6 = new Tag { Name = "Expelliarmus" };
            
            var task1 = new Task {
                 Title = "SavingSiriusBlack",
                 AssignedTo = user1,
                 Description = "You're a wizard Harry!",
                 State = State.Resolved,
                 Tags = new List<Tag> {tag4}
            };
            var task2 = new Task {
                 Title = "FirstClass",
                 AssignedTo = user2,
                 Description = "Start of school",
                 State = State.Closed,
                 Tags = new List<Tag> {tag1, tag2}
            };
            var task3 = new Task {
                 Title = "Teach",
                 AssignedTo = user3,
                 Description = "Learn Ron how to pronounce spells",
                 State = State.Active,
                 Tags = new List<Tag> {tag3, tag4, tag6}
            };
            var task4 = new Task {
                 Title = "Conquer Magical Europe",
                 AssignedTo = user4,
                 Description = "For the Greater Good",
                 State = State.Active,
                 Tags = new List<Tag> {tag1, tag2, tag3, tag4, tag5, tag6}
            };
            var task5 = new Task {
                 Title = "Revenge",
                 AssignedTo = user5,
                 Description = "Kill Harry Potter",
                 State = State.Active,
                 Tags = new List<Tag> {tag5, tag5, tag5}

            };var task6 = new Task {
                 Title = "New Spell Ideas",
                 AssignedTo = user6,
                 Description = "Could be mandatory",
                 State = State.Removed,
                 Tags = new List<Tag> {tag2, tag5}
            }; 

            context.Tasks.AddRange(task1, task2,task3,task4,task5,task6);
            context.Tags.AddRange(tag1,tag2,tag3,tag4,tag5,tag6);
            context.Users.AddRange(user1, user2, user3, user4, user5, user6);

            context.SaveChanges();
        }
    }           
}
