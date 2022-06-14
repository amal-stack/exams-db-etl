namespace SampleConsoleApp1.Models;

public partial class Question
{
    public Question()
    {
        Answers = new HashSet<Answer>();
        ExamQuestions = new HashSet<ExamQuestion>();
    }

    public int Id { get; set; }
    public string Description { get; set; } = null!;

    public virtual ICollection<Answer> Answers { get; set; }
    public virtual ICollection<ExamQuestion> ExamQuestions { get; set; }
}
