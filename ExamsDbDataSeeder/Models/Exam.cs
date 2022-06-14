namespace SampleConsoleApp1.Models;

public partial class Exam
{
    public Exam()
    {
        ExamQuestions = new HashSet<ExamQuestion>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? CourseId { get; set; }
    public DateTime StartTime { get; set; }
    public TimeSpan Duration { get; set; }

    public virtual Course? Course { get; set; }
    public virtual ICollection<ExamQuestion> ExamQuestions { get; set; }
}
