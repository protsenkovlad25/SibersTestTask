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
    public class DeleteController : Controller
    {
        public ProjectContext db;
        public DeleteController(ProjectContext context)
        {
            db = context;
        }

        //Удвление проекта из бд
        public IActionResult DeleteProject(int id)
        {
            Project project = db.Projects.Find(id);

            db.Entry(project).State = EntityState.Deleted;
            db.SaveChanges();

            return RedirectToRoute(new { Controller = "Home", Action = "Projects" });
        }

        //Удаление сотрудника из проекта
        public IActionResult DeleteProjectTeam(int idProject, int idEmployee)
        {
            Project project = db.Projects.Include(t => t.Teams).FirstOrDefault(p => p.Id == idProject);
            Employee employee = db.Employees.FirstOrDefault(e => e.Id == idEmployee);

            if(project != null && employee != null)
            {
                var projectEmployee = project.Teams.FirstOrDefault(pe => pe.EmployeeId == employee.Id);
                project.Teams.Remove(projectEmployee);
                db.SaveChanges();
            }

            return RedirectToRoute(new { Controller = "Details", Action = "DetailsProject", id = idProject });
        }

        //Удаление сотрудника из бд
        public IActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);

            db.Entry(employee).State = EntityState.Deleted;
            db.SaveChanges();

            return RedirectToRoute(new { Controller = "Home", Action = "Employees" });
        }
    }
}
