using System.Text.Json.Serialization;

namespace ExamsDbDataEtl.Models.Extractors;

#nullable disable
public class QuestionJsonModel
{
    public string Question { get; set; }
    public string Distractor3 { get; set; }
    public string Distractor1 { get; set; }
    public string Distractor2 { get; set; }

    [JsonPropertyName("correct_answer")]
    public string CorrectAnswer { get; set; }
    public string Support { get; set; }

    public Question ToQuestion() => new()
    {
        Description = Question,
        Answers = new List<Answer>
        {
            new Answer { Description = Distractor1 },
            new Answer { Description = Distractor2 },
            new Answer { Description = Distractor3 },
            new Answer { Description = CorrectAnswer, IsCorrect = true }
        }
    };
}
#nullable enable