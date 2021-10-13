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
            var entity = new Tag { 
                Name = tag.Name
            };
            _context.Tags.Add(entity);

            _context.SaveChanges();

            return new (Response.Created, entity.Id);
        }
        public IReadOnlyCollection<TagDTO> ReadAll() 
        {
            throw new System.NotImplementedException();
        }
        public TagDTO Read(int tagId) 
        {
            throw new System.NotImplementedException();
        }
        public Response Update(TagUpdateDTO tag)
        {
            throw new System.NotImplementedException();

        }
        public Response Delete(int tagId, bool force = false)
        {
            throw new System.NotImplementedException();
        }


    }
}