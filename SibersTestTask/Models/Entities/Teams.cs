using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SibersTestTask.Models.Entities
{
    //Класс сотрудников по проектам
    public class Teams
    {
        [Required]
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
