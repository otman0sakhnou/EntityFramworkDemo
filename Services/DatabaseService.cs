using EntityFrameWorkTp.Data;
using EntityFrameWorkTp.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using EntityFrameWorkTp.DTOs;
using EntityFrameWorkTp.Models;
using Microsoft.Extensions.Logging;

namespace EntityFrameWorkTp.Services;

public class DatabaseService : IDatabaseService
{
    private readonly AppDbContext _context;

    public DatabaseService(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<TeacherSubjectView>> GetTeacherSubjectsDynamicAsync()
    {
        try
        {
            if (!await _context.Database.CanConnectAsync())
            {
                throw new InvalidOperationException("Cannot connect to database");
            }

            var results = await _context.TeacherSubjects.FromSqlRaw(
                    "SELECT * FROM V_Teacher_Subject")
                .AsNoTracking()
                .ToListAsync();

            return results;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error fetching teacher subjects: {ex.Message}");
            throw;
        }
    }

    public async Task<StudentDetailsDto> GetStudentByNumberAsync(string studentNumber)
    {
        // Input validation
        if (string.IsNullOrWhiteSpace(studentNumber))
        {
            throw new ArgumentException("Student number cannot be empty", nameof(studentNumber));
        }

        try
        {
            // Verify database connection
            if (!await _context.Database.CanConnectAsync())
            {
                throw new InvalidOperationException("Cannot connect to database");
            }

            var param = new SqlParameter("@StudentNumber", studentNumber);

            var results = await _context.Database.SqlQueryRaw<StudentDetailsDto>(
                    "EXEC GetStudentByStudentNumber @StudentNumber",
                    param)
                .AsNoTracking()
                .ToListAsync();

            return results.FirstOrDefault(); 
        }
        catch (SqlException ex) when (ex.Number == 208)
        {
            Debug.WriteLine($"Database object not found: {ex.Message}");
            throw;
        }
        catch (SqlException ex)
        {
            Debug.WriteLine($"SQL Error executing stored procedure: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unexpected error: {ex.Message}");
            throw;
        }
    }
    public async Task<bool> VerifyDatabaseObjectsExist()
    {
        try
        {
            // Check view exists
            await _context.Database.ExecuteSqlRawAsync(
                "IF OBJECT_ID('V_Teacher_Subject', 'V') IS NULL THROW 51000, 'View does not exist', 1");

            // Check procedure exists
            await _context.Database.ExecuteSqlRawAsync(
                "IF OBJECT_ID('GetStudentByStudentNumber', 'P') IS NULL THROW 51000, 'Procedure does not exist', 1");

            return true;
        }
        catch
        {
            return false;
        }
    }
}