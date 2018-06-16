using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task.Models.Entities;

namespace Task.Models.Managers
{
    public class InstructorManager : Repository<Instructor, TaskModel>
    {
        public InstructorManager(TaskModel context) : base(context)
        {
        }
    }
}