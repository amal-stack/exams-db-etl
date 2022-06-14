using Microsoft.EntityFrameworkCore;
using SampleConsoleApp1.Models;
using System.Text.Json;

namespace SampleConsoleApp1;

public class Helpers
{
    public static async Task InsertQuestions()
    {
        //Read the train.json file of the SciQ dataset
        var json = await File.ReadAllTextAsync(@"C:\Users\amalk\Documents\Academics\MCA\Semester 2\DW\data\SciQ\SciQ dataset-2 3\train.json");

        // Deserialize JSON and reshape
        var questions = JsonSerializer.Deserialize<List<QuestionJsonModel>>(json,
            new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            })!
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
            .ToList();

        using var context = new ExamContext();
        // Add questions
        context.AddRange(questions);
        // Save
        await context.SaveChangesAsync();
    }

    public static async Task InsertPrograms()
    {
        // Read CSV file
        string[] lines = await File.ReadAllLinesAsync(@"C:\Users\amalk\Documents\Academics\MCA\Semester 2\DW\data\archive\majors-list.csv");

        // Keeps track of program names
        Dictionary<string, Models.Program> programs = new();

        // Create DbContext (allows interacting with the database)
        using var context = new ExamContext();

        foreach (var line in lines[1..]) // Ignore first line (headers)
        {

            string[] row = line.Split(',',
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.RemoveEmptyEntries);

            Models.Program program;

            if (!programs.TryGetValue(row[2], out program!))
            {
                program = new Models.Program { Name = row[2] };
                programs.Add(row[2], program);
            }

            program.Courses.Add(new Course { Name = row[1] });
        }
        context.AddRange(programs.Values);

        Console.WriteLine(await context.SaveChangesAsync());
    }

    public static async Task InsertExams()
    {
        using var context = new ExamContext();
        var courseIds = await
            (from course in context.Courses
             select course.Id)
            .ToListAsync();

        Random random = new();

        var examNames = new[]
        {
            "Internal Test",
            "Unit Test",
            "Progress Test",
            "Objective Test",
            "Subjective Test"
        };

        foreach (var courseId in courseIds)
        {
            context.Add(CreateExam(random, examNames, courseId));
            context.Add(CreateExam(random, examNames, courseId));
            context.Add(CreateExam(random, examNames, courseId));
        }

        Console.WriteLine(await context.SaveChangesAsync());
    }

    public static async Task InsertStudentPrograms()
    {
        using var context = new ExamContext();
        List<Models.Program> programs = await context.Programs.ToListAsync();
        List<Student> students = await context.Students.ToListAsync();
        Random random = new();

        foreach (Student student in students)
        {
            // Assign 1-3 programs for each student
            foreach (var i in Enumerable.Range(0, random.Next(1, 4)))
            {
                student.Programs.Add(programs[random.Next(programs.Count)]);
            }
        }
        Console.WriteLine(await context.SaveChangesAsync());
    }

    public static async Task InsertExamQuestionResponses()
    {
        using var context = new ExamContext();

        // Fetch all exam questions and group by course
        var examQuestionGroups = await context
            .ExamQuestions
            .AsNoTracking()
            .Include(eq => eq.Exam)
            .Include(eq => eq.Question.Answers)
            .GroupBy(eq => eq.Exam.CourseId)
            .Select(g => new { g.Key, Values = g.ToList() })
            .ToListAsync();

        Random random = new();
        //Console.WriteLine($"EQC: {examQuestionGroups.Count}");
        int i = 0;
        foreach (var examQuestionGroup in examQuestionGroups)
        {
            //var students = await context.Courses
            //    .Where(c => c.Id == examQuestion.Exam.CourseId)
            //    .SelectMany(c => c.Programs)
            //    .SelectMany(p => p.Students)
            //    .ToListAsync();

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

                // Add a random question response for each student
                foreach (var studentId in studentIds)
                {
                    var choice = answers.ElementAt(random.Next(answers.Count));
                    ExamQuestionResponse response = new()
                    {
                        ExamQuestionId = examQuestion.Id,
                        StudentId = studentId,
                        AnswerId = choice.Id,
                        Points = choice.IsCorrect ? 1 : 0
                    };
                    context.Add(response);
                    Console.WriteLine(i++);
                }
            }
        }
        Console.WriteLine("Saving...");
        Console.WriteLine(await context.SaveChangesAsync());
        Console.WriteLine("Saved");
    }

    private static Exam CreateExam(
        Random random,
        string[] examNames,
        int courseId)
    {
        var exam = new Exam
        {
            CourseId = courseId,
            Name = examNames[random.Next(examNames.Length)],
            StartTime = new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Unspecified)
                            .AddDays(random.Next(4500))
                            .AddHours(random.Next(24)),
            Duration = TimeSpan.FromHours(random.Next(10))
        };
        HashSet<int> existingIds = new();
        foreach (var i in Enumerable.Range(1, 50))
        {
            int questionId;
            while (true)
            {
                questionId = random.Next(1, 11680);
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
