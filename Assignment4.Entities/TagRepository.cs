using Assignment4.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace Assignment4.Entities
{
    public class TagRepository : ITagRepository
    {
        public KanbanContext _context {private set; get;}
        public TagRepository(KanbanContext context)
        {
            _context = context;
        }

        public (Response Response, int TagId) Create(TagCreateDTO tag)
        {            
            var entity = new Tag { Name = tag.Name };
            var IdChecker = from Item in _context.Tags
                            where Item.Name == tag.Name
                            select Item.Id;
            var check = IdChecker.FirstOrDefault();
            if (check == 0) {
                _context.Tags.Add(entity);
                _context.SaveChanges();
                return(Response.Created, entity.Id);
            } 
            return (Response.Conflict, check);
        }

        public IReadOnlyCollection<TagDTO> ReadAll() 
        {
            return _context.Tags.Select(t => new TagDTO(t.Id, t.Name)).ToList().AsReadOnly();
        }
        public TagDTO Read(int tagId) 
        {
            var tagById = from t in _context.Tags
                          where t.Id == tagId
                          select new TagDTO(t.Id, t.Name);
            return tagById.FirstOrDefault<TagDTO>();
        }
        public Response Update(TagUpdateDTO tag)
        {
            var entity = _context.Tags.Find(tag.Id);
            if (entity == null) return Response.NotFound;

            entity.Name = tag.Name;
            entity.Id = tag.Id;
            _context.SaveChanges();

            return Response.Updated;
        }
        public Response Delete(int tagId, bool force = false)
        {
            var entity = _context.Tags.Find(tagId);
            if (entity == null) return Response.NotFound;

            var Assigned = _context.Tasks.Any(task => task.Tags.Select(tag => tag.Id).Contains(tagId));
            if (Assigned == true && !force) {
                return Response.Conflict;
            } else {
                _context.Tags.Remove(entity);
                _context.SaveChanges();

                return Response.Deleted;
            }
        }
    }
}