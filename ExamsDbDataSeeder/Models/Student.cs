namespace SampleConsoleApp1.Models;

public partial class Student
{
    public Student()
    {
        ExamQuestionResponses = new HashSet<ExamQuestionResponse>();
        Programs = new HashSet<Program>();
    }

    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateOnly Dob { get; set; }
    public string Email { get; set; } = null!;

    public virtual ICollection<ExamQuestionResponse> ExamQuestionResponses { get; set; }

    public virtual ICollection<Program> Programs { get; set; }
}
