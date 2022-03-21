using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SibersTestTask.Models;
using SibersTestTask.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SibersTestTask.Controllers
{
    public class AddController : Controller
    {
        public ProjectContext db;
        public AddController(ProjectContext context)
        {
            db = context;
        }

        //Добавления нового проекта в бд
        #region AddProject
        public IActionResult AddProject()
        {
            ViewBag.Employees = db.Employees.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult AddProject(Project project)
        {
            //Добавление руководителя в сотрудников проекта
            Employee director = db.Employees.Find(project.DirectorId);
            project.Employees.Add(director);

            //Добавление проекта в бд
            db.Projects.Add(project);
            db.SaveChanges();
            return RedirectToRoute(new { Controller = "Home", Action = "Projects" });
        }
        #endregion

        //Добавление нового сотрудника в бд
        #region AddEmployee
        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToRoute(new { Controller = "Home", Action = "Employees" });
        }
        #endregion

        //Добавление сотрудника в проект
        public IActionResult AddEmployeeProject(int idProject, int[] selectedEmployees)
        {
            Project newProject = db.Projects.Find(idProject);

            if(selectedEmployees != null)
            {
                foreach(var employee in db.Employees.Where(e => selectedEmployees.Contains(e.Id)))
                {
                    newProject.Employees.Add(employee);
                }
            }

            db.Entry(newProject).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToRoute(new { Controller = "Details", Action = "DetailsProject", id = newProject.Id });
        }
    }
}
