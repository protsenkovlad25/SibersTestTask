using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SibersTestTask.Models.Entities
{
    //Класс Сотрудника
    public class Employee
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public string SecondName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string FatherName { get; set; }

        [Required]
        public string Email { get; set; }

        public ICollection<Project> Projects { get; set; }
        public ICollection<Teams> Teams { get; set; }

        public Employee()
        {
            Projects = new List<Project>();
            Teams = new List<Teams>();
        }
    }
}
