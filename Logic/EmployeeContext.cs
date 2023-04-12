using Microsoft.EntityFrameworkCore;

namespace Logic
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee>  Employees { get; set; }

        public EmployeeContext()
        {
        }

        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(
            "Server=localhost;" +
            "Port=5432;Database=employee_sample;" +
            "User Id=postgres;" +
            "Password=123;");

      
    }
}