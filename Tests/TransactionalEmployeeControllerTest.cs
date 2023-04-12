using EmployeeAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [Collection("TransactionalTests")]
    public class TransactionalEmployeeControllerTest : IDisposable
    {
        public TransactionalEmployeeControllerTest(TransactionalTestDatabaseFixture fixture)
        => Fixture = fixture;

        public TransactionalTestDatabaseFixture Fixture { get; }

        [Fact]
        public void UpdateEmployeeAge()
        {
            using var context = Fixture.CreateContext();

            var controller = new EmployeeController(context);
            controller.UpdateEmployeeAge("Filipe Brito", 30);

            var employee = context.Employees.Single(b => b.name == "Filipe Brito");
            Assert.Equal(30, employee.age);
        }

        [Fact]
        public void GetEmplyee()
        {
            //Arrange
            using var context = Fixture.CreateContext();
            var controller = new EmployeeController(context);

            //Act
            var blog = controller.GetEmployee("Filipe Brito").Value;

            //Assert
            Assert.Equal(27, blog?.age);
        }

        [Fact]
        public void AddEmployee()
        {
            using var context = Fixture.CreateContext();
            context.Database.BeginTransaction();

            var controller = new EmployeeController(context);
            controller.AddEmployee("Filipe Brito Dev", 24, "");

            context.ChangeTracker.Clear();

            var employee = context.Employees.Single(b => b.name == "Filipe Brito Dev");
            Assert.Equal(24, employee?.age);
        }

        public void Dispose() => Fixture.Cleanup();
    }
}
