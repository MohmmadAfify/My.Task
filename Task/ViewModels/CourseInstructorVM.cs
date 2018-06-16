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
        public InstructorCourse InstructorCourse { get; set; }

        public int CourseId { get; set; }
        public int CourseCode { get; set; }
        public string CourseName { get; set; }
        public int CourseHours { get; set; }
        public string InstructorName { get; set; }
        public bool CourseHasInstructor { get; set; }
        public bool CourseIsDeleted { get; set; }
        public int InstructorId { get; set; }

        public SelectList Instructors { get; set; }
    }
}