using Assignment4.Core;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;


namespace Assignment4.Entities
{
    public class TaskRepository : ITaskRepository
    {
        private readonly SqlConnection _connection;


        public TaskRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public (Response Response, int TaskId) Create(TaskCreateDTO task) {
            
            /* var cmdText = @"INSERT Tasks (Title, Description, AssignedToId, State)
                            VALUES (@Title, @Description, @AssignedToId, @State)
                            SELECT SCOPE_IDENTITY()";
            
            using var command = new SqlCommand(cmdText, _connection);

            command.Parameters.AddWithValue("@Title", task.Title);
            command.Parameters.AddWithValue("@Description", task.Description);
            command.Parameters.AddWithValue("@AssignedToId", task.AssignedToId);
            command.Parameters.AddWithValue("@State", task.State);

            OpenConnection();

            var id = command.ExecuteScalar();

            CloseConnection();

            return (int) id; */
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<TaskDTO> ReadAll()
        {
            /* var cmdText = @"SELECT c.Id, c.Title, c.Description, c.AssignedToId, c.State
                            FROM Tasks";

            using var command = new SqlCommand(cmdText, _connection);

            OpenConnection();

            using var reader = command.ExecuteReader();

            var list = new List<TaskDTO>();
            while (reader.Read())
            {
                list.Add(new TaskDTO
                {
                    Id = reader.GetInt32("Id"),
                    Title = reader.GetString("Title"),
                    Description = reader.GetString("Description"),
                    AssignedToId = reader.GetInt32("AssignedToId"),
                    State = Enum.Parse<State>(reader.GetString("State"))
                });
            }
            
            CloseConnection();;

            return list; */
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<TaskDTO> ReadAllRemoved() {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByTag(string tag) {
            throw new System.NotImplementedException();
        }
        public IReadOnlyCollection<TaskDTO> ReadAllByUser(int userId) {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByState(State state) {
            throw new System.NotImplementedException();
        }

        public TaskDetailsDTO Read(int TaskId) {
            throw new System.NotImplementedException();
        }

        public Response Update(TaskUpdateDTO task) {
            throw new System.NotImplementedException();
        }
        
        public Response Delete(int TaskId)
        {
            /* var cmdText = @"DELETE Tasks WHERE Id = @Id";

            using var command = new SqlCommand(cmdText, _connection);

            command.Parameters.AddWithValue("@Id", taskId);

            OpenConnection();

            command.ExecuteNonQuery();

            CloseConnection(); */
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        private void OpenConnection()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        private void CloseConnection()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

    }
}
