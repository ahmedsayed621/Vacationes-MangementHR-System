using Microsoft.EntityFrameworkCore;
using Vacationes_MangementHR_System.Models;

namespace Vacationes_MangementHR_System.Data
{
    public class VacationDbContext : DbContext
    {
        public VacationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<VacationType> VacationTypes { get; set; }
        public DbSet<VacationPlan> VacationPlans { get; set; }
        public DbSet<RequestVacation> RequestVacations { get; set; }
        
    }
}
