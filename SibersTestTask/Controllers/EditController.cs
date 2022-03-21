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
    public class EditController : Controller
    {
        public ProjectContext db;
        public EditController(ProjectContext context)
        {
            db = context;
        }

        //Редактирование данных проекта
        #region EditProject
        [HttpGet]
        public IActionResult EditProject(int id)
        {
            Project project = db.Projects.Include(t => t.Teams).FirstOrDefault(p => p.Id == id); ;

            List<Employee> employees = new List<Employee>();

            //Нахождение сотрудников, которых нет в проекте, кроме нынешнего руководителя, и занесение их в ViewBag
            //В представлении через ViewBag этих сотрудников можно выбирать как нового руководителя
            bool emp = false;
            foreach (var dbEmployee in db.Employees.ToList())
            {
                foreach (var team in project.Teams.ToList())
                    if ((team.ProjectId == project.Id) && (team.EmployeeId == dbEmployee.Id))
                        emp = true;

                if (emp == false)
                    employees.Add(dbEmployee);
                else
                    emp = false;
            }
            employees.Add(db.Employees.Find(project.DirectorId));
            ViewBag.Employees = employees;

            return View(project);
        }
        [HttpPost]
        public IActionResult EditProject(Project project, int idDirector)
        {
            //Добавление нового руководителя в проект
            Employee director = db.Employees.Find(project.DirectorId);
            project.Employees.Add(director);
            db.Entry(project).State = EntityState.Modified;

            //Нахождение изменённого проект и старого руководителя
            project = db.Projects.Include(t => t.Teams).FirstOrDefault(p => p.Id == project.Id);
            Employee employee = db.Employees.FirstOrDefault(e => e.Id == idDirector);
            
            //Удаление староого руководителя из проекта
            var oldDirector = project.Teams.FirstOrDefault(pe => pe.EmployeeId == employee.Id);
            project.Teams.Remove(oldDirector);

            db.Entry(project).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToRoute(new { Controller = "Details", Action = "DetailsProject", id = project.Id });
        }
        #endregion

        //Редактирование данных сотрудника
        #region EditEmployee
        [HttpGet]
        public IActionResult EditEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);

            return View(employee);
        }
        [HttpPost]
        public IActionResult EditEmployee(Employee employee)
        {
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToRoute(new { Controller = "Details", Action = "DetailsEmployee", id = employee.Id });
        }
        #endregion
    }
}
