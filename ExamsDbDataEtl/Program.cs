using ExamsDbDataEtl.Core;
using ExamsDbDataEtl.Core.Extractors;
using ExamsDbDataEtl.Extractors.Random;
using ExamsDbDataEtl.Loaders;
using ExamsDbDataEtl.Models;
using ExamsDbDataEtl.Models.Extractors;
using ExamsDbDataEtl.Transformers;
using System.Text.Json;

string datasetsPath = "Datasets";
string questionsFilePath = Path.Combine(datasetsPath, "SciQ", "SciQ dataset-2 3", "train.json");
string majorsFilePath = Path.Combine(datasetsPath, "CollegeMajors", "majors-list.csv");

// Questions and Answers
var questionExtractor = new JsonFileExtractProcess<QuestionJsonModel>(
    questionsFilePath,
    new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    });

var questionEtlPipeline = new EtlPipelineBuilder<QuestionJsonModel, Question>()
    .AddExtractor(questionExtractor)
    .AddTransformer(new QuestionTransformProcess())
    .AddLoader(new LoadIntoExamDatabaseProcess<Question>())
    .Build();

// Programs and Courses
var programExtractor = new CsvFileExtractProcess<MajorCsvModel>(
    majorsFilePath,
    props => new MajorCsvModel(Name: props[1], Category: props[2]));

var programEtlPipeline = new EtlPipelineBuilder<MajorCsvModel, ExamsDbDataEtl.Models.Program>()
    .AddExtractor(programExtractor)
    .AddTransformer(new ProgramTransformProcess())
    .AddLoader(new LoadIntoExamDatabaseProcess<ExamsDbDataEtl.Models.Program>())
    .Build();

// Adding students to programs
var addProgramsEtlProcess = new AddProgramsToStudentEtlProcess();

var studentEtlPipeline = new EtlPipelineBuilder<Student, Student>()
    .AddExtractor(addProgramsEtlProcess)
    .AddTransformer(addProgramsEtlProcess)
    .AddLoader(addProgramsEtlProcess)
    .Build();

// Exams and ExamQuestions
var examEtlPipeline = new EtlPipelineBuilder<Exam, Exam>()
    .AddExtractor(new RandomExamExtractProcess())
    .AddTransformer(TransformProcess.Null<Exam>())
    .AddLoader(new LoadIntoExamDatabaseProcess<Exam>())
    .Build();

// Exam Responses
var responseEtlPipeline = new EtlPipelineBuilder<ExamQuestionResponse, ExamQuestionResponse>()
    .AddExtractor(new RandomExamResponseExtractProcess())
    .AddTransformer(TransformProcess.Null<ExamQuestionResponse>())
    .AddLoader(new LoadIntoExamDatabaseProcess<ExamQuestionResponse>())
    .Build();

// Register event handlers
questionEtlPipeline.RegisterConsoleLoggers();
programEtlPipeline.RegisterConsoleLoggers();
studentEtlPipeline.RegisterConsoleLoggers();
examEtlPipeline.RegisterConsoleLoggers();
responseEtlPipeline.RegisterConsoleLoggers();



await Task.WhenAll(
    questionEtlPipeline.ExecuteAsync(),
    programEtlPipeline.ExecuteAsync());

await studentEtlPipeline.ExecuteAsync();
await examEtlPipeline.ExecuteAsync();
await responseEtlPipeline.ExecuteAsync();

Console.WriteLine("COMPLETED.");
