
using library.Models;
using Npgsql;

namespace library.Data;

// Every SQL statement about feedback lives in this one class.
public class FeedbackRepository
{
    private readonly NpgsqlDataSource _dataSource;

    // ASP.NET Core provides the data source registered in Program.cs.
    public FeedbackRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    private static Feedback ReadFeedback(NpgsqlDataReader reader)
    {
        return new Feedback
        {
            FeedbackId = reader.GetInt64(0),
            FullName = reader.IsDBNull(1) ? null : reader.GetString(1),
            Body = reader.IsDBNull(2) ? null : reader.GetString(2),
            CreatedOn = reader.GetDateTime(3)
        };
    }

    // READ: get all feedback, newest first.
    public async Task<List<Feedback>> GetAllAsync()
    {
        const string sql =
            "SELECT feedback_id, full_name, body, created_on " +
            "FROM lending.feedback ORDER BY created_on DESC;";

        var feedbackList = new List<Feedback>();

        await using var command = _dataSource.CreateCommand(sql);
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            feedbackList.Add(ReadFeedback(reader));
        }

        return feedbackList;
    }

    // CREATE: add feedback. The database generates feedback_id and created_on.
    public async Task AddAsync(Feedback feedback)
    {
        const string sql =
            "INSERT INTO lending.feedback (full_name, body) " +
            "VALUES (@full_name, @body);";

        await using var command = _dataSource.CreateCommand(sql);
        command.Parameters.AddWithValue(
            "full_name", (object?)feedback.FullName ?? DBNull.Value);
        command.Parameters.AddWithValue(
            "body", (object?)feedback.Body ?? DBNull.Value);

        await command.ExecuteNonQueryAsync();
    }

    // READ: get one feedback entry, or null if not found.
    public async Task<Feedback?> GetByIdAsync(long id)
    {
        const string sql =
            "SELECT feedback_id, full_name, body, created_on " +
            "FROM lending.feedback WHERE feedback_id = @id;";

        await using var command = _dataSource.CreateCommand(sql);
        command.Parameters.AddWithValue("id", id);

        await using var reader = await command.ExecuteReaderAsync();

        return await reader.ReadAsync() ? ReadFeedback(reader) : null;
    }

    // UPDATE: change one feedback entry.
    public async Task UpdateAsync(Feedback feedback)
    {
        const string sql =
            "UPDATE lending.feedback " +
            "SET full_name = @full_name, body = @body " +
            "WHERE feedback_id = @id;";

        await using var command = _dataSource.CreateCommand(sql);
        command.Parameters.AddWithValue(
            "full_name", (object?)feedback.FullName ?? DBNull.Value);
        command.Parameters.AddWithValue(
            "body", (object?)feedback.Body ?? DBNull.Value);
        command.Parameters.AddWithValue("id", feedback.FeedbackId);

        await command.ExecuteNonQueryAsync();
    }

    // DELETE: remove one feedback entry.
    public async Task DeleteAsync(long id)
    {
        const string sql =
            "DELETE FROM lending.feedback WHERE feedback_id = @id;";

        await using var command = _dataSource.CreateCommand(sql);
        command.Parameters.AddWithValue("id", id);

        await command.ExecuteNonQueryAsync();
    }
}
