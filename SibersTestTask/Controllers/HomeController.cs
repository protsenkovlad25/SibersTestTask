using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SibersTestTask.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SibersTestTask.Controllers
{
    public class HomeController : Controller
    {
        public ProjectContext db;
        public HomeController(ProjectContext context)
        {
            db = context;
        }

        //Главная страница
        public IActionResult Index()
        {
            return View();
        }

        //Страница всех проектов
        public IActionResult Projects()
        {
            return View(db.Projects.ToList());
        }

        //Страница всех сотрудников
        public IActionResult Employees()
        {
            return View(db.Employees.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
