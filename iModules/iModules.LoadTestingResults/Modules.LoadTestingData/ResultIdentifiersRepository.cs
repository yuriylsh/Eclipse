using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Modules.LoadTestingData
{
    public class ResultIdentifiersRepository
    {
        private readonly string _connectionString;

        public ResultIdentifiersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<(IEnumerable<ResultIdentifier>, int)> GetInitialResultsAsync(int pageIndex, int pageSize)
        {
            var results = new List<ResultIdentifier>(pageSize);
            int totalCount;
            var sql = string.Format(Sql.ResultRepositoryGetInitialResultsFormat, CalculateOffset(pageIndex, pageSize), pageSize);
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                await connection.OpenAsync();
                using (var resultsReader = await command.ExecuteReaderAsync())
                {
                    while (resultsReader.Read())
                    {
                        results.Add(new ResultIdentifier
                        {
                            Id = (Guid)resultsReader[0],
                            Date = (DateTimeOffset)resultsReader[1],
                            Name = GetNullableString(resultsReader[2])
                        });
                    }
                    await resultsReader.NextResultAsync();
                    resultsReader.Read();
                    totalCount = (int) resultsReader[0];
                }
            }
            return (results, totalCount);
        }

        public async Task<IEnumerable<ResultIdentifier>> GetResultsAsync(int pageIndex, int pageSize)
        {
            var sql = string.Format(Sql.ResultRepositoryGetResultsFormat, CalculateOffset(pageIndex, pageSize), pageSize);
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<ResultIdentifier>(sql);
            }
        }

        private int CalculateOffset(int pageIndex, int pageSize) => (pageIndex - 1) * pageSize;


        private static string GetNullableString(object dataReaderValue) => dataReaderValue is DBNull ? null : (string)dataReaderValue;

        public async Task SetResultName(Guid id, string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(Sql.ResultRepositorySetName, connection))
            {
                await connection.OpenAsync();
                command.Parameters.Add("@newName", SqlDbType.NVarChar, 300).Value = name;
                command.Parameters.Add("@id", SqlDbType.NVarChar).Value = id.ToString();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
