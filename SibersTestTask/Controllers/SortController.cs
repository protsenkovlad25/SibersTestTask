using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SibersTestTask.Models;
using SibersTestTask.Models.Entities;
using SibersTestTask.Models.Sort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SibersTestTask.Controllers
{
    public class SortController : Controller
    {
        public ProjectContext db;
        public SortController(ProjectContext context)
        {
            db = context;
        }

        //Сортировка проектов
        public async Task<IActionResult> SortProject(SortProjectState sortOrder = SortProjectState.NameAsc)
        {
            IQueryable<Project> projects = db.Projects.Include(p => p.Employees);

            //Переменные для хранения данных проектов
            ViewData["NameSort"] = sortOrder == SortProjectState.NameAsc ? SortProjectState.NameDesc : SortProjectState.NameAsc;
            ViewData["PrioritySort"] = sortOrder == SortProjectState.PriorityAsc ? SortProjectState.PriorityDesc : SortProjectState.PriorityAsc;
            ViewData["CustomerCompanySort"] = sortOrder == SortProjectState.CustomerCompanyAsc ? SortProjectState.CustomerCompanyDesc : SortProjectState.CustomerCompanyAsc;
            ViewData["ContractorCompanySort"] = sortOrder == SortProjectState.ContractorCompanyAsc ? SortProjectState.ContractorCompanyDesc : SortProjectState.ContractorCompanyAsc;
            ViewData["DirectorSort"] = sortOrder == SortProjectState.DirectorAsc ? SortProjectState.DirectorDesc : SortProjectState.DirectorAsc;

            //Сортировка проектов в соответствии с переданным значением
            projects = sortOrder switch
            {
                SortProjectState.NameDesc => projects.OrderByDescending(s => s.Name),
                SortProjectState.PriorityAsc => projects.OrderBy(s => s.Priority),
                SortProjectState.PriorityDesc => projects.OrderByDescending(s => s.Priority),
                SortProjectState.CustomerCompanyAsc => projects.OrderBy(s => s.CustomerCompany),
                SortProjectState.CustomerCompanyDesc => projects.OrderByDescending(s => s.CustomerCompany),
                SortProjectState.ContractorCompanyAsc => projects.OrderBy(s => s.ContractorCompany),
                SortProjectState.ContractorCompanyDesc => projects.OrderByDescending(s => s.ContractorCompany),
                SortProjectState.DirectorAsc => projects.OrderBy(s => s.DirectorId),
                SortProjectState.DirectorDesc => projects.OrderByDescending(s => s.DirectorId),
                _ => projects.OrderBy(s => s.Name)
            };

            return View(await projects.AsNoTracking().ToListAsync());
        }
    }
}
