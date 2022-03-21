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
    public class DetailsController : Controller
    {
        public ProjectContext db;
        public DetailsController(ProjectContext context)
        {
            db = context;
        }

        //Содержимое проекта
        public IActionResult DetailsProject(int id)
        {
            Project project = db.Projects.Include(c => c.Employees).FirstOrDefault(p => p.Id == id);

            if (project != null)
            {
                List<Employee> employees = new List<Employee>();

                //Нахождение сотруников, которых нет в проекте и занесение их в ViewBag
                //В представлении через ViewBag эти сотрудники выводятся, чтобы можно было добавлять новых сотрудников в проект
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
                ViewBag.Employees = employees;
                
                return View(project);
            }
            return NotFound();
        }

        //Детали сотрудника
        public IActionResult DetailsEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);

            if (employee != null)
            {
                return View(employee);
            }
            return NotFound();
        }
    }
}
