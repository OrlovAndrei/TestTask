using System.Security.Cryptography.X509Certificates;

namespace TestTask.Domain
{
	public interface IEmployeeRepository
	{
		public void Insert(Employee employee);
		public List<Employee> Select();
		public Employee Select(int id);
		public void Update(Employee employee);
		public void Delete(int id);
	}
}
