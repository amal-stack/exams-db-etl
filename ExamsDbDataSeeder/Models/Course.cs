namespace SampleConsoleApp1.Models;

public partial class Course
{
    public Course()
    {
        Exams = new HashSet<Exam>();
        Programs = new HashSet<Program>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<Exam> Exams { get; set; }

    public virtual ICollection<Program> Programs { get; set; }
}
