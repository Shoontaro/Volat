using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Volat.Models;

namespace Volat.Data
{
    internal class DataRepository
    {
        private readonly DBService _database;

        public DataRepository(DBService database)
        {
            _database = database;
        }

        public async Task<IEnumerable<Workout>> GetAllAsync()
        {
            using var connection = _database.CreateConnection();

            return await connection.QueryAsync<Workout>(
                "SELECT * FROM Workouts ORDER BY CreatedAt DESC");
        }

        public async Task AddAsync(Workout workout)
        {
            using var connection = _database.CreateConnection();

            const string sql = """
            INSERT INTO Workouts
            (Name, CreatedAt, DurationMinutes)
            VALUES
            (@Name, @CreatedAt, @DurationMinutes)
            """;

            await connection.ExecuteAsync(sql, workout);
        }
    }
}
