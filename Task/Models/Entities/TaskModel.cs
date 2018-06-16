namespace Task.Models.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TaskModel : DbContext
    {
        public TaskModel()
            : base("name=TaskModel")
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<InstructorCourse> InstructorCourses { get; set; }
        public virtual DbSet<InstructorStudent> InstructorStudents { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasMany(e => e.InstructorCourses)
                .WithOptional(e => e.Course)
                .HasForeignKey(e => e.Fk_CourseID);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.StudentCourses)
                .WithOptional(e => e.Course)
                .HasForeignKey(e => e.Fk_CourseId);

            modelBuilder.Entity<Instructor>()
                .HasMany(e => e.InstructorCourses)
                .WithOptional(e => e.Instructor)
                .HasForeignKey(e => e.Fk_InstructorId);

            modelBuilder.Entity<Instructor>()
                .HasMany(e => e.InstructorStudents)
                .WithOptional(e => e.Instructor)
                .HasForeignKey(e => e.Fk_InstructorId);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.InstructorStudents)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.Fk_StudentId);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentCourses)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.Fk_StudentId);

            
        }
    }
}
