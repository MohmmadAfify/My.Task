using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.Models;
using Task.Models.Entities;
using Task.ViewModels;

namespace Task.Controllers
{
    public class InstructorController : Controller
    {
        UnitOfWork unit = new UnitOfWork();
        private TaskModel ctx = new TaskModel();

        public ActionResult Index()
        {
            var instructors = unit.InstructorManager.GetAllBind();
            return View(instructors);
        }

        public ActionResult Details(int id)
        {
            var instructor = unit.InstructorManager.GetById(id);

            // get the course who is teached this instructor
            var courseId = instructor.InstructorCourses.Where(a => a.Fk_InstructorId == id).
                                Select(a => a.Fk_CourseID).ToList();
            var courses = ctx.Courses.Where(a => courseId.Contains(a.Id));

            CourseInstructorVM courseVM = new CourseInstructorVM
            {
                InstructorId = instructor.Id,
                InstructorName = instructor.Name,
                InstructorDept = instructor.Department,
                InstructorMail = instructor.Mail,
                InstructorPhone = instructor.Phone,
                Courses = new SelectList(items: courses,
                                                dataValueField: "Name",
                                                dataTextField: "Name",
                                                selectedValue: courses)
            };

            return View(courseVM);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                unit.InstructorManager.Add(instructor);
                return RedirectToAction("Index");
            }
            return View(instructor);
        }

        public ActionResult Edit(int id)
        {
            Instructor instructor = unit.InstructorManager.GetById(id);
            return View(instructor);
        }

        [HttpPost]
        public ActionResult Edit(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                Instructor old = ctx.Instructors.FirstOrDefault(a => a.Id == instructor.Id);
                old.Name = instructor.Name;
                old.Department = instructor.Department;
                old.Mail = instructor.Mail;
                old.Phone = instructor.Phone;
                unit.InstructorManager.Edit(instructor);
                return RedirectToAction("Index");
            };
            return View();
        }

        public ActionResult Delete(int id)
        {
            Instructor instructor = unit.InstructorManager.GetById(id);
            return View(instructor);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructor instructor = unit.InstructorManager.GetById(id);
            instructor.IsDeleted = true;
            unit.InstructorManager.Delete(instructor.Id);
            return RedirectToAction("Index");
        }
    }
}