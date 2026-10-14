using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace KursProjDemo
{
    public static class DatabaseService
    {
        private const string ConnectionString = "Data Source=exercises.db;Version=3;";
        public static void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS ExerciseHistory (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        MethodName TEXT NOT NULL,
                        CompletionDate TEXT NOT NULL,
                        IsCompleted INTEGER DEFAULT 0
                    );";

                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        public static int AddExerciseRecord(string methodName)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string insertQuery = @"
            INSERT INTO ExerciseHistory (MethodName, CompletionDate, IsCompleted)
            VALUES (@MethodName, @CompletionDate, @IsCompleted);
            SELECT last_insert_rowid();";

                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@MethodName", methodName);
                    command.Parameters.AddWithValue("@CompletionDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@IsCompleted", 0);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public static void UpdateExerciseCompletion(int id, bool isCompleted)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE ExerciseHistory SET IsCompleted = @IsCompleted WHERE Id = @Id;";

                using (var command = new SQLiteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@IsCompleted", isCompleted ? 1 : 0);
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
        public static List<ExerciseRecord> GetExerciseHistory()
        {
            var history = new List<ExerciseRecord>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Id, MethodName, CompletionDate, IsCompleted FROM ExerciseHistory;";

                using (var command = new SQLiteCommand(selectQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        history.Add(new ExerciseRecord
                        {
                            MethodName = reader["MethodName"].ToString(),
                            CompletionDate = DateTime.Parse(reader["CompletionDate"].ToString()),
                            IsCompleted = Convert.ToBoolean(reader["IsCompleted"])
                        });
                    }
                }
            }

            return history;
        }
    }
}