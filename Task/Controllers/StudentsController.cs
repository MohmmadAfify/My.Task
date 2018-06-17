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
    public class StudentsController : Controller
    {
        private TaskModel ctx = new TaskModel();
        UnitOfWork unit = new UnitOfWork();


        public ActionResult Index()
        {
            List<Student> students = unit.StudentManager.GetAllBind();
            return View(students);
        }

        public ActionResult Details(int id)
        {
            #region Past Code
            //Student student = unit.StudentManager.GetById(id);
            //return View(student);
            #endregion

            Student student = unit.StudentManager.GetById(id);

            List<Course> courses = unit.CourseManager.GetAllBind();
            List<Instructor> instructors = unit.InstructorManager.GetAllBind();

            var courseId = student.StudentCourses.Where(a => a.Fk_StudentId == id).
                                Select(a => a.Fk_CourseId).ToList();
            courses = ctx.Courses.Where(a => courseId.Contains(a.Id)).ToList();

            StudentCourseVM studentCourseVM = new StudentCourseVM
            {
                Courses = new List<Course>(),
                StudentName = student.Name,
                StudentId = student.Id,
            };
            TempData["studentId"] = id;
            studentCourseVM.Courses = courses;
            return View(studentCourseVM);
        }

        [HttpPost, ActionName("Detail")]
        public ActionResult FullDetail(int courseId)
        {
            int studentId = Convert.ToInt32(TempData.Peek("studentId"));
            Student student = unit.StudentManager.GetById(studentId);

            List<Instructor> instructors = unit.InstructorManager.GetAllBind();

            // Want to get the instructor who teach the speciefic course 
            //  FOR SOME REASON IT DIDN'T FITCH ALL INSTRUCTORS EVERY TIME
            var instructorId = student.InstructorStudents.Where(a => a.Fk_StudentId == studentId).
                                Where(c => c.Instructor.InstructorCourses.FirstOrDefault().Fk_CourseID == courseId).
                                Select(a => a.Fk_InstructorId).ToList();
            instructors = ctx.Instructors.Where(a => instructorId.Contains(a.Id)).ToList();

            SelectList instructorsList = new SelectList(instructors, "Id", "Name", "");
            return Json(instructorsList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                unit.StudentManager.Add(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        public ActionResult Edit(int id)
        {
            Student student = unit.StudentManager.GetById(id);
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                Student old = ctx.Students.FirstOrDefault(s => s.Id == student.Id);
                old.Name = student.Name;
                old.Mail = student.Mail;
                old.Phone = student.Phone;
                old.BirthDate = student.BirthDate;
                unit.StudentManager.Edit(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        public ActionResult Delete(int id)
        {
            Student student = unit.StudentManager.GetById(id);
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = unit.StudentManager.GetById(id);
            student.IsDeleted = true;
            unit.StudentManager.Delete(student.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Enroll(int id)
        {
            Student student = unit.StudentManager.GetById(id);
            List<Course> courses = unit.CourseManager.GetAllBind();
            List<Instructor> instructors = unit.InstructorManager.GetAllBind();

            StudentCourseVM studentCourseVM = new StudentCourseVM
            {
                Courses = new List<Course>(),
                StudentName = student.Name,
                StudentId = student.Id,
            };

            studentCourseVM.Courses = courses;
            return View(studentCourseVM);
        }

        [HttpPost, ActionName("Enroll")]
        public ActionResult EnrollCompleted(int courseId)
        {
            List<Instructor> instructors = unit.InstructorManager.GetAllBind();
            instructors = ctx.InstructorCourses.Where(a => a.Fk_CourseID == courseId).Select(i => i.Instructor).ToList();

            SelectList instructorsList = new SelectList(instructors, "Id", "Name", "");
            TempData["courseId"] = courseId;
            return Json(instructorsList);
        }

        [HttpPost]
        public ActionResult AssignCourse(int StudentId, int instructorID)
        {
            #region Logic Here
            /*  Get the instuctorId, StudentId, CourseId
                    *  assign the instuctorId, StudentId in instructorstudent entity
                    *  assign the StudentId, CourseId in studentcourse entity
                    */
            #endregion

            int courseId = Convert.ToInt32(TempData["courseId"]);

            StudentCourse studentCourse = new StudentCourse
            {
                Fk_CourseId = courseId,
                Fk_StudentId = StudentId
            };
            ctx.StudentCourses.Add(studentCourse);

            InstructorStudent instructorStudent = new InstructorStudent
            {
                Fk_InstructorId = instructorID,
                Fk_StudentId = StudentId
            };
            ctx.InstructorStudents.Add(instructorStudent);

            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
