using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.Models.Entities;

namespace Task.ViewModels
{
    public class CourseInstructorVM
    {
        public Course Course { get; set; }
        public Instructor Instructor { get; set; }
        public InstructorCourse InstructorCourse { get; set; }

        public int Id { get; set; }
        public int Code { get; set; }
        public string CourseName { get; set; }
        public int Hours { get; set; }
        public string InstructorName { get; set; }

        public SelectList Instructors { get; set; }
    }
}