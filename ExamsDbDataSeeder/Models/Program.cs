namespace SampleConsoleApp1.Models;

public partial class Program
{
    public Program()
    {
        Courses = new HashSet<Course>();
        Students = new HashSet<Student>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; }
    public virtual ICollection<Student> Students { get; set; }
}
