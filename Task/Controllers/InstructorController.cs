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
        UnitOfWork Unit = new UnitOfWork();
        private TaskModel ctx = new TaskModel();

        public ActionResult Index()
        {
            var instructors = Unit.InstructorManager.GetAllBind();
            return View(instructors);
        }

        public ActionResult Detail(int id)
        {
            return View();
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
                Unit.InstructorManager.Add(instructor);
                return RedirectToAction("Index");
            }
            return View(instructor);
        }

        public ActionResult Edit(int id)
        {
            Instructor instructor = Unit.InstructorManager.GetById(id);
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
                Unit.InstructorManager.Edit(instructor);
                return RedirectToAction("Index");
            };
            return View();
        }

        public ActionResult Delete(int id)
        {
            Instructor instructor = Unit.InstructorManager.GetById(id);
            return View(instructor);
        }

        [HttpPost]
        public ActionResult Delete(Instructor instructor)
        {
            instructor.IsDeleted = true;
            Unit.InstructorManager.Delete(instructor.Id);
            return RedirectToAction("Index");
        }
    }
}