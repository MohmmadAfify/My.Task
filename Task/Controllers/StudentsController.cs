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
            Student student = unit.StudentManager.GetById(id);
            return View(student);
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

        [HttpPost]
        public ActionResult Delete(Student student)
        {
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
                CourseId = 0,
                InstructorId = 0                
            };
            studentCourseVM.Courses = courses;

            #region Past Code
            //StudentCourseVM studentCourseVM = new StudentCourseVM
            //{
            //    StudentId = student.Id,
            //    StudentName = student.Name,
            //    Courses = new SelectList(items: courses,
            //                             dataValueField: "Id",
            //                             dataTextField: "Name",
            //                             selectedValue: courses),
            //    FilteredInstructors = new SelectList(items: instructors,
            //                                 dataValueField: "Id",
            //                                 dataTextField: "Name",
            //                                 selectedValue: instructors)
            //}; 
            #endregion

            return View(studentCourseVM);
        }

        [HttpPost, ActionName("Enroll")]
        public ActionResult EnrollCompleted(/*StudentCourseVM studentCourseVM*/ int courseId)
        {
            List<Instructor> instructors = unit.InstructorManager.GetAllBind();
            instructors = ctx.InstructorCourses.Where(a => a.Fk_CourseID == courseId).Select(i => i.Instructor).ToList();

            SelectList instructorsList = new SelectList(instructors, "Id", "Name", "");
            TempData["courseId"] = courseId;
            return Json(instructorsList);

            #region worked
            //List<Instructor> instructors = unit.InstructorManager.GetAllBind();
            //instructors = ctx.InstructorCourses.Where(a => a.Fk_CourseID == courseId).Select(i => i.Instructor).ToList();
            //SelectList instructorsList = new SelectList(instructors, "Id", "Name", "");

            #endregion
        }

        [HttpPost]
        public ActionResult AssignCourse(int StudentId, int ddlInstructor)
        {
            int courseId = Convert.ToInt32(TempData["courseId"]);
            #region Logic Here
            /*  Get the instuctorId, StudentId, CourseId
                    *  assign the instuctorId, StudentId in instructorstudent entity
                    *  assign the StudentId, CourseId in studentcourse entity
                    */
            #endregion
            #region MyRegion
            // StudentCourseVM studentCourseVM = new StudentCourseVM();
            //{
            //    InstructorStudent = ctx.InstructorStudents.FirstOrDefault(),
            //    StudentCourse = ctx.StudentCourses.FirstOrDefault()
            //};
            //var instructorstudent = studentCourseVM.InstructorStudent;
            //instructorstudent.Fk_StudentId = 0;
            //instructorstudent.Fk_InstructorId = 0;

            //instructorstudent.Fk_StudentId = StudentId;
            //instructorstudent.Fk_InstructorId = StudentId;

            //var studentcourse = studentCourseVM.StudentCourse;
            //studentcourse.Fk_CourseId = courseId;
            //studentcourse.Fk_StudentId = StudentId;
            //studentcourse.FirstOrDefault().Fk_CourseId = courseId;
            //studentcourse.FirstOrDefault().Fk_StudentId = StudentId;
            //ctx.StudentCourses.Add(studentcourse.FirstOrDefault());

            //instructorstudent.FirstOrDefault().Fk_InstructorId = ddlInstructor;
            //instructorstudent.FirstOrDefault().Fk_StudentId = StudentId;
            //ctx.InstructorStudents.Add(instructorstudent.FirstOrDefault());

            //var studentcourse = studentCourseVM.StudentCourse;
            //var instructorstudent = studentCourseVM.InstructorStudent;

            //studentcourse.Fk_CourseId = courseId;
            //studentcourse.Fk_StudentId = StudentId;
            //ctx.StudentCourses.Add(studentcourse);

            //instructorstudent.Fk_InstructorId = ddlInstructor;
            //instructorstudent.Fk_StudentId = StudentId;
            //ctx.InstructorStudents.Add(instructorstudent);
            #endregion

            StudentCourse studentCourse = new StudentCourse
            {
                Fk_CourseId = courseId,
                Fk_StudentId = StudentId
            };
            ctx.StudentCourses.Add(studentCourse);

            InstructorStudent instructorStudent = new InstructorStudent
            {
                Fk_InstructorId = ddlInstructor,
                Fk_StudentId = StudentId
            };
            ctx.InstructorStudents.Add(instructorStudent);

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
