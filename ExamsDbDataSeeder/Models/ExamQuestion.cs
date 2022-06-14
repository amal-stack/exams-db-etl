namespace SampleConsoleApp1.Models;

public partial class ExamQuestion
{
    public ExamQuestion()
    {
        ExamQuestionResponses = new HashSet<ExamQuestionResponse>();
    }

    public int Id { get; set; }
    public int? ExamId { get; set; }
    public int? QuestionId { get; set; }

    public virtual Exam? Exam { get; set; }
    public virtual Question? Question { get; set; }
    public virtual ICollection<ExamQuestionResponse> ExamQuestionResponses { get; set; }
}
