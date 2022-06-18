using ExamsDbDataEtl.Abstractions;
using ExamsDbDataEtl.Data;
using ExamsDbDataEtl.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamsDbDataEtl.Extractors.Random;

/// <summary>
/// Creates random exams for each existing course in the exams database.
/// </summary>
public class RandomExamExtractProcess : IAsyncExtractProcess<Exam>
{
    /// <summary>
    /// Gets or sets the maximum number of days that can be added to the start time.
    /// </summary>
    public int MaxNumberOfDays { get; set; } = 4500;

    /// <summary>
    /// Gets or sets the number of questions included in each exam.
    /// </summary>
    public int NumberOfQuestions { get; set; } = 100;

    /// <summary>
    /// Gets or sets the list of exam names from which the name of exam will be randomly picked.
    /// </summary>
    public string[] ExamNames { get; set; } =
    {
        "Internal Test",
        "Unit Test",
        "Progress Test",
        "Objective Test",
        "Subjective Test"
    };

    /// <summary>
    /// Gets or sets the number of exams to be generated for each course.
    /// </summary>
    public int ExamsPerCourse { get; set; } = 3;

    /// <summary>
    /// Gets or sets the lower bound of the exam start time that will be generated.
    /// </summary>
    public DateTime StartTime { get; set; } =
        new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);



    public async Task<IList<Exam>> ExtractAsync()
    {
        System.Random random = new();
        List<Exam> exams = new();

        IReadOnlyList<int> courseIds;
        IReadOnlyList<int> questionIds;
        using (var context = new ExamContext())
        {
            courseIds = await
               (from course in context.Courses
                select course.Id)
               .ToListAsync();
            questionIds = await
                (from question in context.Questions
                 select question.Id)
                 .ToListAsync();
        }

        foreach (var courseId in courseIds)
        {
            for (int i = 0; i < ExamsPerCourse; i++)
            {
                exams.Add(CreateExam(
                    random,
                    ExamNames,
                    StartTime,
                    MaxNumberOfDays,
                    courseId,
                    questionIds,
                    NumberOfQuestions
                    ));
            }
        }
        return await Task.FromResult(exams);
    }


    internal static Exam CreateExam(
        System.Random random,
        string[] examNames,
        DateTime startTime,
        int maxNumberOfDays,
        int courseId,
        IReadOnlyList<int> questionIds,
        int numberOfQuestions)
    {
        var exam = new Exam
        {
            CourseId = courseId,
            Name = examNames[random.Next(examNames.Length)],
            StartTime = startTime
                            .AddDays(random.Next(maxNumberOfDays))
                            .AddHours(random.Next(24)),
            Duration = TimeSpan.FromHours(random.Next(10))
        };
        HashSet<int> existingIds = new();
        foreach (var _ in Enumerable.Range(1, numberOfQuestions))
        {
            int questionId;
            while (true)
            {
                questionId = questionIds[random.Next(questionIds.Count)];
                if (!existingIds.Contains(questionId))
                {
                    existingIds.Add(questionId);
                    break;
                }
            }
            exam.ExamQuestions.Add(new()
            {
                QuestionId = questionId
            });
        }
        return exam;
    }
}
