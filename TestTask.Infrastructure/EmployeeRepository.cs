using FluentValidation;
using TestTask.Domain;

namespace TestTask.Infrastructure
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly AppDbContext _appDbContext;
		private readonly EmployeeValidator _validator;

		public EmployeeRepository(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
			_validator = new EmployeeValidator();
		}

		public void Insert(Employee employee)
		{
			Validate(employee);
			_appDbContext.Employees.Add(employee);
			_appDbContext.SaveChanges();
		}

		public List<Employee> Select()
		{
			return _appDbContext.Employees.ToList();
		}

		public Employee Select(int id)
		{
			return _appDbContext.Employees.Find(id) ?? throw new Exception("There is no employee with this Id");
		}

		public void Update(Employee updatedEmployee)
		{
			var employee = Select(updatedEmployee.Id);
			employee.FullName = updatedEmployee.FullName;
			employee.Position = updatedEmployee.Position;
			Validate(employee);
			_appDbContext.Employees.Update(employee);
			_appDbContext.SaveChanges();
		}

		public void Delete(int id)
		{
			_appDbContext.Employees.Remove(Select(id));
			_appDbContext.SaveChanges();
		}

		private void Validate(Employee employee)
		{
			if (_appDbContext.Employees.Where(e => e.FullName == employee.FullName && e.Id != employee.Id).Any())
				throw new Exception("An employee with this full name already exists");
			_validator.ValidateAndThrow(employee);
		}
	}
}
