using ExamsDbDataEtl.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamsDbDataEtl.Data;

public partial class ExamContext : DbContext
{
    public ExamContext()
    {
    }

    public ExamContext(DbContextOptions<ExamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; } = null!;
    public virtual DbSet<Course> Courses { get; set; } = null!;
    public virtual DbSet<Exam> Exams { get; set; } = null!;
    public virtual DbSet<ExamQuestion> ExamQuestions { get; set; } = null!;
    public virtual DbSet<ExamQuestionResponse> ExamQuestionResponses { get; set; } = null!;
    public virtual DbSet<Instructor> Instructors { get; set; } = null!;
    public virtual DbSet<Models.Program> Programs { get; set; } = null!;
    public virtual DbSet<Question> Questions { get; set; } = null!;
    public virtual DbSet<Student> Students { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            optionsBuilder.UseNpgsql(ConnectionStringConfiguration.GetConnectionString());
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.ToTable("answer");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Description).HasColumnName("description");

            entity.Property(e => e.IsCorrect).HasColumnName("is_correct");

            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.Question)
                .WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("answer_question_id_fkey");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("course");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.ToTable("exam");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.CourseId).HasColumnName("course_id");

            entity.Property(e => e.Duration).HasColumnName("duration");

            entity.Property(e => e.Name).HasColumnName("name");

            entity.Property(e => e.StartTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_time");

            entity.HasOne(d => d.Course)
                .WithMany(p => p.Exams)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("exam_course_id_fkey");
        });

        modelBuilder.Entity<ExamQuestion>(entity =>
        {
            entity.ToTable("exam_question");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.ExamId).HasColumnName("exam_id");

            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.Exam)
                .WithMany(p => p.ExamQuestions)
                .HasForeignKey(d => d.ExamId)
                .HasConstraintName("exam_question_exam_id_fkey");

            entity.HasOne(d => d.Question)
                .WithMany(p => p.ExamQuestions)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("exam_question_question_id_fkey");
        });

        modelBuilder.Entity<ExamQuestionResponse>(entity =>
        {
            entity.ToTable("exam_question_response");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.AnswerId).HasColumnName("answer_id");

            entity.Property(e => e.ExamQuestionId).HasColumnName("exam_question_id");

            entity.Property(e => e.Points).HasColumnName("points");

            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Answer)
                .WithMany(p => p.ExamQuestionResponses)
                .HasForeignKey(d => d.AnswerId)
                .HasConstraintName("exam_question_response_answer_id_fkey");

            entity.HasOne(d => d.ExamQuestion)
                .WithMany(p => p.ExamQuestionResponses)
                .HasForeignKey(d => d.ExamQuestionId)
                .HasConstraintName("exam_question_response_exam_question_id_fkey");

            entity.HasOne(d => d.Student)
                .WithMany(p => p.ExamQuestionResponses)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("exam_question_response_student_id_fkey");
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.ToTable("instructor");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.DateJoined).HasColumnName("date_joined");

            entity.Property(e => e.Dob).HasColumnName("dob");

            entity.Property(e => e.Email).HasColumnName("email");

            entity.Property(e => e.FirstName).HasColumnName("first_name");

            entity.Property(e => e.LastName).HasColumnName("last_name");
        });

        modelBuilder.Entity<Models.Program>(entity =>
        {
            entity.ToTable("program");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasMany(d => d.Courses)
                .WithMany(p => p.Programs)
                .UsingEntity<Dictionary<string, object>>(
                    "ProgramCourse",
                    l => l.HasOne<Course>().WithMany().HasForeignKey("CourseId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("program_course_course_id_fkey"),
                    r => r.HasOne<Models.Program>().WithMany().HasForeignKey("ProgramId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("program_course_program_id_fkey"),
                    j =>
                    {
                        j.HasKey("ProgramId", "CourseId").HasName("program_course_pkey");

                        j.ToTable("program_course");

                        j.IndexerProperty<int>("ProgramId").HasColumnName("program_id");

                        j.IndexerProperty<int>("CourseId").HasColumnName("course_id");
                    });

            entity.HasMany(d => d.Students)
                .WithMany(p => p.Programs)
                .UsingEntity<Dictionary<string, object>>(
                    "ProgramStudent",
                    l => l.HasOne<Student>().WithMany().HasForeignKey("StudentId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("program_student_student_id_fkey"),
                    r => r.HasOne<Models.Program>().WithMany().HasForeignKey("ProgramId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("program_student_program_id_fkey"),
                    j =>
                    {
                        j.HasKey("ProgramId", "StudentId").HasName("program_student_pkey");

                        j.ToTable("program_student");

                        j.IndexerProperty<int>("ProgramId").HasColumnName("program_id");

                        j.IndexerProperty<int>("StudentId").HasColumnName("student_id");
                    });
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.ToTable("question");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Description).HasColumnName("description");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("student");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Dob).HasColumnName("dob");

            entity.Property(e => e.Email).HasColumnName("email");

            entity.Property(e => e.FirstName).HasColumnName("first_name");

            entity.Property(e => e.LastName).HasColumnName("last_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
