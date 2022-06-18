namespace ExamsDbDataEtl.Models;

public partial class Answer
{
    public Answer()
    {
        ExamQuestionResponses = new HashSet<ExamQuestionResponse>();
    }

    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public bool IsCorrect { get; set; }
    public int? QuestionId { get; set; }

    public virtual Question? Question { get; set; }
    public virtual ICollection<ExamQuestionResponse> ExamQuestionResponses { get; set; }
}
