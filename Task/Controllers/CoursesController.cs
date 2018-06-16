using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Task.Models;
using Task.Models.Entities;
using Task.ViewModels;

namespace Task.Controllers
{
    public class CoursesController : Controller
    {
        UnitOfWork unit = new UnitOfWork();
        private TaskModel ctx = new TaskModel();

        public ActionResult Index()
        {
            #region Logic Here
            /* Get all the courses without binding to InstructorCourse
                    * In Delete we'll check if the course is binded to any instructors
                    */
            #endregion
            var courses = unit.CourseManager.GetAllBind();
            return View(courses);
        }

        public ActionResult Details(int id)
        {
            #region Logic Here
                /*
                        *Dispaly the course with drop downlist of its instructors
                        */
            #endregion

            var course = unit.CourseManager.GetById(id);

            // get the instructors who teach this course
            var instructorId = course.InstructorCourses.Where(a => a.Fk_CourseID == id).
                                Select(a => a.Fk_InstructorId).ToList();
            var instructors = ctx.Instructors.Where(a => instructorId.Contains(a.Id));

            CourseInstructorVM courseVM = new CourseInstructorVM
            {
                CourseId = course.Id,
                CourseName = course.Name,
                CourseCode = course.Code,
                CourseHours = course.Hours ?? 20,
                CourseHasInstructor = course.HasInstructor,
                Instructors = new SelectList(items: instructors,
                                                dataValueField: "Name",
                                                dataTextField: "Name", 
                                                selectedValue: instructors)
            };

            return View(courseVM);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                unit.CourseManager.Add(course);
                return RedirectToAction("Index");
            }
            return View(course);
        }

        public ActionResult Edit(int id)
        {
            Course course = unit.CourseManager.GetById(id);
            return View(course);
        }

        [HttpPost]
        public ActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                Course old = ctx.Courses.FirstOrDefault(a => a.Id == course.Id);
                // Unit.CourseManager.GetById(course.Id); ** Conflict with dbset.Attach
                old.HasInstructor = course.HasInstructor;
                old.Name = course.Name;
                old.Hours = course.Hours;
                old.Code = course.Code;
                unit.CourseManager.Edit(course);
                return RedirectToAction("Index");
            }
            return View(course);
        }

        public ActionResult Delete(int id)
        {
            Course course = unit.CourseManager.GetById(id);
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = unit.CourseManager.GetById(id);
            course.IsDeleted = true;
            unit.CourseManager.Delete(id);
            return RedirectToAction("Index");
        }

        [ActionName("Assign")]
        public ActionResult AssignCourseToInstructor(int id)
        {
            Course course = unit.CourseManager.GetById(id);
            List<Instructor> instructors = unit.InstructorManager.GetAllBind();

            CourseInstructorVM courseVM = new CourseInstructorVM
            {
                CourseId = course.Id,
                CourseName = course.Name,
                Instructors = new SelectList(items: instructors,
                                             dataValueField: "Id",
                                             dataTextField: "Name",
                                             selectedValue: instructors)
            };
            return View(courseVM);
        }

        [HttpPost , ActionName("Assign")]
        public ActionResult AssignCourseToInstructor(CourseInstructorVM courseInstructorVM)
        {
            // get entities
            var course = ctx.Courses.FirstOrDefault(c => c.Id == courseInstructorVM.CourseId);
            var instructor = ctx.Instructors.FirstOrDefault(c => c.Id == courseInstructorVM.InstructorId);
            var InstructorCourse = courseInstructorVM.InstructorCourse;

            // Make them equal to Zero because it always sent as "NULL"
            InstructorCourse.Fk_CourseID = 0;
            InstructorCourse.Fk_InstructorId = 0;

            course.HasInstructor = true;
            InstructorCourse.Fk_InstructorId = instructor.Id;
            InstructorCourse.Fk_CourseID = course.Id;

            ctx.InstructorCourses.Add(InstructorCourse);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
