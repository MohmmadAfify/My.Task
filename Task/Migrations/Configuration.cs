namespace Task.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Task.Models.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<Task.Models.Entities.TaskModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Task.Models.Entities.TaskModel context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //context.Courses.AddOrUpdate(
            //      new Course { Name = "C#", Code = 101, No__of_Hours = 30 },
            //      new Course { Name = "Java", Code = 201, No__of_Hours = 30 },
            //      new Course { Name = "JavaScript", Code = 301, No__of_Hours = 20 },
            //      new Course { Name = "HTML", Code = 401, No__of_Hours = 10 },
            //      new Course { Name = "CSS", Code = 501, No__of_Hours = 10 }
            //    );

            //context.Instructors.AddOrUpdate(
            //    new Instructor { Name = "Ahmed Ali", Department = "A", Mail = "a@a.com" },
            //    new Instructor { Name = "Mostafa Ahmed", Department = "B", Mail = "h@h.com" }
            //    new Instructor { Name = "Halah Hamdy", Department = "C", Mail = "a@a.com" }
            //    );

            //context.Students.AddOrUpdate(
            //    new Student { Name = "Mohammad Ali", Mail = "ma@m.com", BirthDate = new DateTime(1991, 6, 6) },
            //    new Student { Name = "Mahmoud Fekry", Mail = "mb@m.com", BirthDate = new DateTime(1992, 4, 6) },
            //    new Student { Name = "Mohammad Hossam", Mail = "mt@m.com", BirthDate = new DateTime(1991, 5, 1) },
            //    new Student { Name = "Ahmad Kamal", Mail = "ah@m.com", BirthDate = new DateTime(1994, 11, 21) }
            //    );
        }
    }
}
