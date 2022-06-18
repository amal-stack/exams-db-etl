using ExamsDbDataEtl.Abstractions;
using ExamsDbDataEtl.Models;
using ExamsDbDataEtl.Models.Extractors;

namespace ExamsDbDataEtl.Transformers;


public class QuestionTransformProcess : IAsyncTransformProcess<QuestionJsonModel, Question>
{
    public async Task<IList<Question>> TransformAsync(IList<QuestionJsonModel> values)
        => await Task.FromResult(
            values
            .Select(q => new Question
            {
                Description = q.Question,
                Answers = new List<Answer>
                {
                    new Answer { Description = q.Distractor1 },
                    new Answer { Description = q.Distractor2 },
                    new Answer { Description = q.Distractor3 },
                    new Answer { Description = q.CorrectAnswer, IsCorrect = true }
                }
            })
           .ToList());
}
