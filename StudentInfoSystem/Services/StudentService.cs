using Microsoft.EntityFrameworkCore;
using StudentInfoSystem.Data;
using StudentInfoSystem.Models;

namespace StudentInfoSystem.Services
{
    public class StudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<List<Student>> GetFilteredStudentsAsync(string? nameFilter, decimal? minMarks, decimal? maxMarks, string? gradeFilter, string? sortField = null, bool isAscending = true)
        {
            var query = _context.Students.AsQueryable();

            // Apply filters - using SQL-translatable queries
            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                query = query.Where(s => s.Name.ToLower().Contains(nameFilter.ToLower()));
            }

            if (minMarks.HasValue)
            {
                query = query.Where(s => s.Marks >= minMarks.Value);
            }

            if (maxMarks.HasValue)
            {
                query = query.Where(s => s.Marks <= maxMarks.Value);
            }

            if (!string.IsNullOrWhiteSpace(gradeFilter))
            {
                query = query.Where(s => s.Grade.ToLower() == gradeFilter.ToLower());
            }

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(sortField))
            {
                query = sortField.ToLower() switch
                {
                    "name" => isAscending ? query.OrderBy(s => s.Name) : query.OrderByDescending(s => s.Name),
                    "age" => isAscending ? query.OrderBy(s => s.Age) : query.OrderByDescending(s => s.Age),
                    "marks" => isAscending ? query.OrderBy(s => s.Marks) : query.OrderByDescending(s => s.Marks),
                    "grade" => isAscending ? query.OrderBy(s => s.Grade) : query.OrderByDescending(s => s.Grade),
                    _ => query.OrderBy(s => s.Name)
                };
            }
            else
            {
                query = query.OrderBy(s => s.Name); // Default sorting by name
            }

            return await query.ToListAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student?> UpdateStudentAsync(Student student)
        {
            var existingStudent = await _context.Students.FindAsync(student.Id);
            if (existingStudent == null)
                return null;

            existingStudent.Name = student.Name;
            existingStudent.Age = student.Age;
            existingStudent.Marks = student.Marks;
            existingStudent.Grade = student.Grade;

            await _context.SaveChangesAsync();
            return existingStudent;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<string>> GetDistinctGradesAsync()
        {
            return await _context.Students
                .Select(s => s.Grade)
                .Distinct()
                .OrderBy(g => g)
                .ToListAsync();
        }

        public async Task<decimal> GetAverageMarksAsync()
        {
            return await _context.Students.AverageAsync(s => s.Marks);
        }

        public async Task<int> GetTotalStudentsAsync()
        {
            return await _context.Students.CountAsync();
        }
    }
}
