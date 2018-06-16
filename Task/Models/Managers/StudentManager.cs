using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task.Models.Entities;

namespace Task.Models.Managers
{
    public class StudentManager : Repository<Student, TaskModel>
    {
        public StudentManager(TaskModel context) : base(context)
        {
        }
    }
}