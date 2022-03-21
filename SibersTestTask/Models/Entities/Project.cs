using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SibersTestTask.Models.Entities
{
    //Класс проекта
    public class Project
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Priority { get; set; }

        [Required]
        public string CustomerCompany { get; set; }

        [Required]
        public string ContractorCompany { get; set; }

        [Required]
        public int DirectorId { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public ICollection<Employee> Employees { get; set; }
        public ICollection<Teams> Teams { get; set; }

        public Project()
        {
            Employees = new List<Employee>();
            Teams = new List<Teams>();
        }
    }
}
