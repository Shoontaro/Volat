using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Volat
{
    internal class DBService
    {
        private readonly string _connectionString;

        public DBService()
        {
            var dbPath = Path.Combine(
                FileSystem.AppDataDirectory,
                "volat.db");

            _connectionString = $"Data Source={dbPath}";
        }

        public SqliteConnection CreateConnection()
           => new(_connectionString);

        public async Task InitializeAsync() //создаем бд, если такой нет
        {
            const string sql = """
        CREATE TABLE IF NOT EXISTS Workouts
        (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT NOT NULL,
            CreatedAt TEXT NOT NULL,
            DurationMinutes INTEGER NOT NULL
        );
        """;

            await using var connection = CreateConnection();

            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = sql;

            await command.ExecuteNonQueryAsync();
        }
    }
}
