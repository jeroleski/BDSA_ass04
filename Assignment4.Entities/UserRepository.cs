using Assignment4.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Assignment4.Entities
{
    public class UserRepository : IUserRepository
    {
        public KanbanContext _context {private set; get;}
        public UserRepository(KanbanContext context)
        {
            _context = context;
        }

        public (Response Response, int UserId) Create(UserCreateDTO user)
        {
            var entity = new User { 
                Name = user.Name,
                Email = user.Email };

            _context.Users.Add(entity);

            _context.SaveChanges();

            return new (Response.Created, entity.Id);
        }

        public IReadOnlyCollection<UserDTO> ReadAll() =>
            _context.Users
                    .Select(u => new UserDTO(u.Id, u.Name, u.Email))
                    .ToList().AsReadOnly();

        public UserDTO Read(int userId)
        {
            var users = from u in _context.Users
                         where u.Id == userId
                         select new UserDTO(u.Id, u.Name, u.Email);

            return users.FirstOrDefault();
        }

        public Response Update(UserUpdateDTO user)
        {
            var entity = _context.Users.Find(user.Id);
            if (entity == null){
                return Response.NotFound;
            }

            entity.Name = user.Name;
            entity.Email = user.Email;

            _context.SaveChanges();
            return Response.Updated;
        }


        public Response Delete(int userId, bool force = false)
        {
            var entity = _context.Users.Find(userId);
            if (entity == null){
                return Response.NotFound;
            }

            _context.Users.Remove(entity);
            _context.SaveChanges();

            return Response.Deleted;
        }
    }
}