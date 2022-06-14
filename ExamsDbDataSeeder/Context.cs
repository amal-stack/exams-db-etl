
//public class Context : DbContext
//{
//    protected override void OnConfiguring(DbContextOptionsBuilder options)
//    {
//        options.UseNpgsql("Server=localhost;Port=5432;Database=exams;User Id=user1;Password=user1");
//    }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {

//        var question = modelBuilder.Entity<Question>();
//        question.ToTable("question");
//        question.Property(q => q.Id).HasColumnName("id");
//        question.Property(q => q.Description).HasColumnName("description");

//        var answer = modelBuilder.Entity<Answer>();
//        answer.ToTable("answer");
//        answer.Property(a => a.Id).HasColumnName("id");
//        answer.Property(a => a.QuestionId).HasColumnName("question_id");
//        answer.Property(a => a.Description).HasColumnName("description");
//        answer.Property(a => a.IsCorrect).HasColumnName("is_correct");

//        var course = modelBuilder.Entity<Course>();
//        course.ToTable("course");
//        course.Property(p => p.Id).HasColumnName("id");
//        course.Property(p => p.Name).HasColumnName("name");

//        var student = modelBuilder.Entity<Student>();
//        student.ToTable("student");
//        student.Property(student => student.Id).HasColumnName("id");
//        student.Property(student => student.FirstName).HasColumnName("first_name");
//        student.Property(student => student.LastName).HasColumnName("last_name");
//        student.Property(student => student.DateOfBirth).HasColumnName("dob");
//        student.Property(student => student.Email).HasColumnName("email");

//        var instructor = modelBuilder.Entity<Instructor>();
//        instructor.ToTable("instructor");
//        instructor.Property(i => i.Id).HasColumnName("id");
//        instructor.Property(i => i.FirstName).HasColumnName("first_name");
//        instructor.Property(i => i.LastName).HasColumnName("last_name");
//        instructor.Property(i => i.DateOfBirth).HasColumnName("dob");
//        instructor.Property(i => i.Email).HasColumnName("email");
//        instructor.Property(i => i.DateJoined).HasColumnName("date_joined");

//        var exam = modelBuilder.Entity<Exam>();
//        exam.ToTable("exam");
//        exam.Property(e => e.Id).HasColumnName("id");
//        exam.Property(e => e.CourseId).HasColumnName("course_id");
//        exam.Property(e => e.StartTime).HasColumnName("start_time");
//        exam.Property(e => e.Duration).HasColumnName("duration");

//        exam
//            .HasMany(e => e.Questions)
//            .WithMany(q => q.Exams)
//            .UsingEntity(j => j.ToTable("exam_question"));



//        var program = modelBuilder.Entity<AcademicProgram>();
//        program.ToTable("program");
//        program.Property(p => p.Id).HasColumnName("id");
//        program.Property(p => p.Name).HasColumnName("name");

//        program
//            .HasMany(p => p.Courses)
//            .WithMany(p => p.Programs)
//            .UsingEntity(j => j.ToTable("program_course"));

//        program
//            .HasMany(p => p.Students)
//            .WithMany(s => s.Programs)
//            .UsingEntity(j => j.ToTable("program_student"));





//        base.OnModelCreating(modelBuilder);
//    }
//#nullable disable
//    public DbSet<Question> Questions { get; set; }

//    public DbSet<Answer> Answers { get; set; }
//#nullable enable
//}
