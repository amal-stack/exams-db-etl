namespace ExamsDbDataEtl.Models;

public partial class ExamQuestionResponse
{
    public int Id { get; set; }
    public int? ExamQuestionId { get; set; }
    public int? StudentId { get; set; }
    public int? AnswerId { get; set; }
    public int Points { get; set; }

    public virtual Answer? Answer { get; set; }
    public virtual ExamQuestion? ExamQuestion { get; set; }
    public virtual Student? Student { get; set; }
}
