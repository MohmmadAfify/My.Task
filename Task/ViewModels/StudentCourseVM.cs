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
        public List<Course> Courses { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
    }
}