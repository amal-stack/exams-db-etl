using ExamsDbDataEtl.Abstractions;
using ExamsDbDataEtl.Data;
using ExamsDbDataEtl.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamsDbDataEtl.Extractors.Random;

/// <summary>
/// Creates random exam responses for each student part of the course for each exam question.
/// </summary>
public class RandomExamResponseExtractProcess : IAsyncExtractProcess<ExamQuestionResponse>
{
    public int CorrectPoints { get; set; } = 1;

    public int IncorrectPoints { get; set; } = 0;

    public async Task<IList<ExamQuestionResponse>> ExtractAsync()
    {
        using var context = new ExamContext();
        System.Random random = new();
        List<ExamQuestionResponse> responses = new();

        // Fetch all exam questions and group by course
        var examQuestionGroups = await context
            .ExamQuestions
            .AsNoTracking()
            .Include(eq => eq.Exam)
            .Include(eq => eq.Question.Answers)
            .GroupBy(eq => eq.Exam.CourseId)
            .Select(g => new { g.Key, Values = g.ToList() })
            .ToListAsync();

        foreach (var examQuestionGroup in examQuestionGroups)
        {
            // Fetch all students part of the course
            var studentIds = await context
                .Students
                .AsNoTracking()
                .Where(s => s.Programs
                        .SelectMany(p => p.Courses)
                        .Any(c => c.Id == examQuestionGroup.Key))
                .Select(s => s.Id)
                .ToListAsync();

            foreach (var examQuestion in examQuestionGroup.Values)
            {
                var answers = examQuestion.Question.Answers;
                var correctAnswer = answers.First(a => a.IsCorrect);
                var incorrectAnswers = answers.Where(a => !a.IsCorrect).ToList();
                // Add a random question response for each student
                foreach (var studentId in studentIds)
                {
                    // 60-40 chances of correct-incorrect
                    bool pickCorrect = random.Next(10) > 3;

                    var choice = pickCorrect
                        ? correctAnswer
                        : incorrectAnswers[random.Next(incorrectAnswers.Count)];
                    ExamQuestionResponse response = new()
                    {
                        ExamQuestionId = examQuestion.Id,
                        StudentId = studentId,
                        AnswerId = choice.Id,
                        Points = choice.IsCorrect ? CorrectPoints : IncorrectPoints
                    };
                    responses.Add(response);
                }
            }
        }
        return responses;
    }
}
