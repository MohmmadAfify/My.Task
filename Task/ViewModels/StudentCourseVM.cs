using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.Models.Entities;

namespace Task.ViewModels
{
    public class StudentCourseVM
    {
        public Course Course { get; set; }
        public Instructor Instructor { get; set; }
        public StudentCourse StudentCourse { get; set; }
        public InstructorStudent InstructorStudent { get; set; }

        // public SelectList Courses { get; set; }
        public List<Course> Courses { get; set; }
        public SelectList FilteredInstructors { get; set; }

        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int InstructorId { get; set; }
        public int CourseId { get; set; }
    }
}