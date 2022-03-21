using Microsoft.EntityFrameworkCore;
using SibersTestTask.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SibersTestTask.Models
{
    //Класс взаимодействия с бд
    public class ProjectContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Teams> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(local);Database=SibersTestTask;Trusted_Connection=True;");
        }

        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Создание связи многое ко многим между сотрудниками и проектами
            modelBuilder.Entity<Project>()
                .HasMany(c => c.Employees)
                .WithMany(s => s.Projects)
                .UsingEntity<Teams>(
                    j => j
                        .HasOne(pt => pt.Employee)
                        .WithMany(t => t.Teams)
                        .HasForeignKey(pt => pt.EmployeeId),
                    j => j
                        .HasOne(pt => pt.Project)
                        .WithMany(t => t.Teams)
                        .HasForeignKey(pt => pt.ProjectId),
                    j =>
                    {
                        j.HasKey(t => new { t.ProjectId, t.EmployeeId });
                        j.ToTable("Teams");
                    });
        }
    }
}
