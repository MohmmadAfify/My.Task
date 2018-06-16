using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task.Models.Entities;
using Task.Models.Managers;

namespace Task.Models
{
    public class UnitOfWork
    {
        TaskModel ctx = new TaskModel();

        public CourseManager CourseManager
        {
            get
            {
                return new CourseManager(ctx);
            }
        }

        public InstructorManager InstructorManager
        {
            get
            {
                return new InstructorManager(ctx);
            }
        }

        public StudentManager StudentManager
        {
            get
            {
                return new StudentManager(ctx);
            }
        }
    }
}