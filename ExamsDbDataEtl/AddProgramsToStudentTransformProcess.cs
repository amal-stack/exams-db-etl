using ExamsDbDataEtl.Abstractions;
using ExamsDbDataEtl.Data;
using ExamsDbDataEtl.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamsDbDataEtl.Transformers;

public sealed class AddProgramsToStudentEtlProcess :
    IDisposable,
    IAsyncTransformProcess<Student, Student>,
    IAsyncExtractProcess<Student>,
    IAsyncLoadProcess<Student>
{
    private ExamContext? _context;

    /// <summary>
    /// Maximum number of programs to be assigned for one student.
    /// </summary>
    public int MaxPrograms { get; set; } = 3;

    public async Task<IList<Student>> ExtractAsync()
    {
        _context ??= new ExamContext();

        return await _context.Students.ToListAsync();
    }

    public async Task<IList<Student>> TransformAsync(IList<Student> students)
    {
        if (_context is null)
        {
            throw new InvalidOperationException("DbContext is null");
        }

        List<Models.Program> programs = await _context.Programs.ToListAsync();

        Random random = new();
        foreach (Student student in students)
        {
            // Assign 1-MaxPrograms programs for each student
            foreach (var _ in Enumerable.Range(0, random.Next(1, MaxPrograms + 1)))
            {
                student.Programs.Add(programs[random.Next(programs.Count)]);
            }
        }
        return students;
    }

    public async Task<int> LoadAsync(IEnumerable<Student> entities)
    {
        if (_context is null)
        {
            throw new InvalidOperationException("DbContext is null");
        }
        return await _context.SaveChangesAsync();
    }

    public void Dispose() => _context?.Dispose();
}