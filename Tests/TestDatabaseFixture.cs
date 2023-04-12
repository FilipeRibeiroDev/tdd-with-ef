using Logic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class TestDatabaseFixture
    {
        private const string ConnectionString = @"Server=localhost;" +
                                                  "Port=5432;Database=employee_sample;" +
                                                  "User Id=postgres;" +
                                                  "Password=123;";

        private static readonly object _lock = new();
        private static bool _databaseInitialized;

        public TestDatabaseFixture()
        {
            lock(_lock)
            {
                if(!_databaseInitialized) 
                {
                    using var context = CreateContext();
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    context.AddRange(
                        new Employee("Filipe Brito", 27, ""),
                        new Employee("Adriane Brito", 27, ""));
                    context.SaveChanges();
                }
            }
        }

        public EmployeeContext CreateContext() 
            => new EmployeeContext(
              new DbContextOptionsBuilder<EmployeeContext>()
              .UseNpgsql(ConnectionString)
              .Options);

    }
}
