using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task.Models.Entities;

namespace Task.Models.Managers
{
    public class CourseManager : Repository<Course, TaskModel>
    {
        public CourseManager(TaskModel context) : base(context)
        {
        }
    }
}