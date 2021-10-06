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

        public IReadOnlyCollection<TaskDTO> All()
        {
            var cmdText = @"SELECT c.Id, c.Title, c.Description, c.AssignedToId, c.State
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

            return list;
        }

        public int Create(TaskDTO task) {
            
            var cmdText = @"INSERT Tasks (Title, Description, AssignedToId, State)
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

            return (int) id;
        }

        public void Delete(int taskId)
        {
            var cmdText = @"DELETE Tasks WHERE Id = @Id";

            using var command = new SqlCommand(cmdText, _connection);

            command.Parameters.AddWithValue("@Id", taskId);

            OpenConnection();

            command.ExecuteNonQuery();

            CloseConnection();
        }

        public TaskDetailsDTO FindById(int id)
        {
            var cmdText = @"SELECT t.Id, t.Title, t.Description, t.AssignedToId, t.State, 
                            FROM Characters AS c
                            LEFT JOIN Actors AS a ON c.ActorId = a.Id
                            WHERE c.Name = @Name";

            using var command = new SqlCommand(cmdText, _connection);

            var cmdText1 = @"SELECT Task From Tasks WHERE Id = @Id";

            command.Parameters.AddWithValue("@Name", name);

            OpenConnection();

            using var reader = command.ExecuteReader();

            var character = reader.Read()
                ? new TaskDetailsDTO
                {
                    Id = reader.GetInt32("Id"),
                    Title = reader.GetString("Title"),
                    Description = reader.GetString("Description"),
                    AssignedToId = reader.GetInt32("AssignedToId"),
                    AssignedToName = reader.GetString("AssignedToName"),
                    AssignedToEmail = reader.GetString("AssignedToEmail"),
                    Tags = reader.GetInt32("Tags"),
                    State = Enum.Parse<State>(reader.GetString("State"))
                }
                : null;

            CloseConnection();

            return character;
        }
        
        public void Update(TaskDTO task)
        {
                        var cmdText = @"UPDATE Tasks SET
                            Title = @Title,
                            Description = @Description,
                            AssignedToId = @AssignedToId
                            State = @State
                            WHERE Id = @Id";

            using var command = new SqlCommand(cmdText, _connection);

            command.Parameters.AddWithValue("@Id", task.Id);
            command.Parameters.AddWithValue("@Title", task.Title);
            command.Parameters.AddWithValue("@Description", task.Description);
            command.Parameters.AddWithValue("@AssignedToId", task.AssignedToId);
            command.Parameters.AddWithValue("@State", task.State);

            OpenConnection();

            command.ExecuteNonQuery();

            CloseConnection();
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
