using System;
using Microsoft.EntityFrameworkCore;

namespace Assignment4.Entities
{
    public interface IKanbanContext : IDisposable
    {
        public DbSet<Tag> Tags { get;  }
        public DbSet<User> Users { get; }
        public DbSet<Task> Tasks { get; }
        int SaveChanges();
    }
}