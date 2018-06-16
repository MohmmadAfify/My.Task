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
        UnitOfWork Unit = new UnitOfWork();
        private TaskModel ctx = new TaskModel();

        public ActionResult Index()
        {
            #region Logic Here
            /* Get all the courses without binding to InstructorCourse
             * In Delete we'll check if the course is binded to any instructors

            Hint : Try to make DDL of instructors page*/
            #endregion

            #region Past Logic
            // var courses = Unit.CourseManager.GetAll().ToList();
            //var courses = ctx.Courses.ToList();
            //var courseVM = new List<CourseInstructorVM>();
            //var instructors = ctx.Instructors.ToList();

            //foreach (var item in courses)
            //{
            //    courseVM.Add(new CourseInstructorVM ()
            //    {
            //        Id = item.Id,
            //        CourseName = item.Name,
            //        Code = item.Course.Code,
            //        Hours = item.Course.Hours ?? 20,
            //        //InstructorName = item.Instructor.Name
            //        Instructors = new SelectList(items: instructors, dataValueField: item.Instructor.Id.ToString() ,dataTextField: item.Instructor.Name.ToString(), selectedValue: item.Instructor.Name)
            //    });
            //}
            #endregion
            var courses = Unit.CourseManager.GetAllBind();
            return View(courses);
        }

        public ActionResult Details(int id)
        {
            #region Logic Here
            /*
             *Dispaly the course with drop downlist of instructors
             */
            #endregion

            var course = Unit.CourseManager.GetById(id);
            //var instructors = Unit.InstructorManager.GetById(id);

            var instructorId = course.InstructorCourses.Where(a => a.Fk_CourseID == id).
                                Select(a => a.Fk_InstructorId).ToList();

            var instructors = ctx.Instructors.Where(a => instructorId.Contains(a.Id));
            //var instructor = ctx.Instructors.Select(a=>a).Contains( Id==instructorId)
                    //Find(instructorId);
                    // Unit.InstructorManager.GetById(test);

            CourseInstructorVM courseVM = new CourseInstructorVM
            {
                Id = course.Id,
                CourseName = course.Name,
                Code = course.Code,
                Hours = course.Hours ?? 20,
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
                #region Past Logic
                //var courseVM = new List<CourseInstructorVM>();
                //courseVM.Add(new CourseInstructorVM
                //{
                //    Code = course.Code,
                //    CourseName = course.Name,
                //    Hours = course.Hours ?? 20,
                //    InstructorName = instructor.Name
                //});
                //ctx.InstructorCourses.AddRange(courseVM);
                //ctx.Courses.Add(course);
                //ctx.SaveChanges();
                #endregion
                // solve the damn problem
                Unit.CourseManager.Add(course);
                return RedirectToAction("Index");
            }

            return View(course);
        }

        public ActionResult Edit(int id)
        {
            Course course = Unit.CourseManager.GetById(id);
                //ctx.Courses.FirstOrDefault(a => a.Id == id);
            return View(course);
        }

        [HttpPost]
        public ActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                Course old = ctx.Courses.FirstOrDefault(a => a.Id == course.Id);
                // Unit.CourseManager.GetById(course.Id);
                course.HasInstructor = old.HasInstructor;
                old.Name = course.Name;
                old.Hours = course.Hours;
                old.Code = course.Code;
                Unit.CourseManager.Edit(course);
                return RedirectToAction("Index");
            }
            return View(course);
        }

        public ActionResult Delete(int id)
        {
            Course course = Unit.CourseManager.GetById(id);
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = Unit.CourseManager.GetById(id);
            Unit.CourseManager.Delete(id);
            return RedirectToAction("Index");
        }

        [ActionName("Assign")]
        public ActionResult AssignCourseToInstructor(int id)
        {
            Course course = Unit.CourseManager.GetById(id);
            List<Instructor> instructors = Unit.InstructorManager.GetAllBind();

            CourseInstructorVM courseVM = new CourseInstructorVM
            {
                Id = course.Id,
                CourseName = course.Name,
                Instructors = new SelectList(items: instructors,
                                             dataValueField: "Id",
                                             dataTextField: "Name",
                                             selectedValue: instructors)
            };
            return View(courseVM);
        }

        [HttpPost , ActionName("Assign")]
        public ActionResult AssignCourse(CourseInstructorVM courseInstructorVM)
        {
            
            var course = ctx.Courses.FirstOrDefault(c => c.Id == courseInstructorVM.Id);
            var instructor = ctx.Instructors.FirstOrDefault(c => c.Id == courseInstructorVM.Instructor.Id);
            var viewModel = courseInstructorVM.InstructorCourse;

            // Make them equal to Zero because it always sent as "NULL"
            viewModel.Fk_CourseID = 0;
            viewModel.Fk_InstructorId = 0;

            course.HasInstructor = true;
            viewModel.Fk_InstructorId = instructor.Id;
            viewModel.Fk_CourseID = course.Id;
            ctx.InstructorCourses.Add(viewModel);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
