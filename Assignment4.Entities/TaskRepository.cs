using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Assignment4.Core;


namespace Assignment4.Entities
{
    public class TaskRepository : ITaskRepository
    {
        public KanbanContext _context {private set; get;}
        public TaskRepository(KanbanContext context)
        {
            _context = context;
        }

        public (Response Response, int TaskId) Create(TaskCreateDTO task) {

            if (_context.Users.Find(task.AssignedToId) == null) {
                return (Response.BadRequest, -1);
            } else {
                try {
                    var entity = new Task {
                        Title = task.Title,
                        AssignedTo = _context.Users.Find(task.AssignedToId),
                        Description = task.Description,
                        State = State.New,
                        Tags = task.Tags.Select(t => new Tag(){Name = t}).ToList(),
                        Created = DateTime.Now,
                        StatusUpdated = DateTime.Now
                    };
                            
                    _context.Tasks.Add(entity);
                    _context.SaveChanges();

                    return (Response.Created, entity.Id);
                } catch (Exception e) {
                    return (Response.Conflict,_context.Users.Find(task.AssignedToId).Id);
                }
            }
        }

        public IReadOnlyCollection<TaskDTO> ReadAll()
        {
            return _context.Tasks.Select(task => new TaskDTO(
                task.Id,
                task.Title,
                task.AssignedTo.Name,
                task.Tags.Select(tags => tags.Name).ToList(),
                task.State)).ToList().AsReadOnly();
        }

        public IReadOnlyCollection<TaskDTO> ReadAllRemoved() {
            return ReadAllByState(State.Removed);
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByTag(string tag) {        
            return ReadAll().Where(t => t.Tags.Contains(tag)).ToList().AsReadOnly();

        }
        public IReadOnlyCollection<TaskDTO> ReadAllByUser(int userId) {
            return ReadAll().Where(t => t.Id == userId).ToList().AsReadOnly();
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByState(State state) {
            return ReadAll().Where(t => t.State == state).ToList().AsReadOnly();
        }

        public TaskDetailsDTO Read(int TaskId) {
            var tasks =  from t in _context.Tasks
                        where t.Id == TaskId
                        select new TaskDetailsDTO(
                            t.Id,
                            t.Title,
                            t.Description,
                            t.Created,
                            t.AssignedTo.Name,
                            t.Tags.Select(tags => tags.Name).ToList().AsReadOnly(),
                            t.State,
                            t.StatusUpdated
                        );
            return tasks.FirstOrDefault();
        }

        public Response Update(TaskUpdateDTO task) {
            
            var entity = _context.Tasks.Find(task.Id);
            if (entity == null) return Response.NotFound;

            
            entity.Title = task.Title;
            entity.AssignedTo = _context.Users.Find(task.AssignedToId);
            entity.Description = task.Description;
            entity.State = task.State;
            entity.Tags = task.Tags.Select(t => new Tag() {
                Name = t}).ToList();
            entity.StatusUpdated = DateTime.Now;

            _context.SaveChanges();
            return Response.Updated;

        }
        
        public Response Delete(int TaskId)
        {
            var entity = _context.Tasks.Find(TaskId);
            if (entity == null) return Response.NotFound;

            if (entity.State == State.New || entity.State == State.Active) {
                entity.State = State.Removed;
                _context.Tasks.Remove(entity);
                _context.SaveChanges();
                return Response.Deleted;
            } else {
                return Response.Conflict;
            }
        }
    }
}
