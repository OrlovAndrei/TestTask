using Microsoft.AspNetCore.Mvc;
using TestTask.API.Models;
using TestTask.Domain;

namespace TestTask.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class EmployeeController : ControllerBase
	{
		private readonly IEmployeeRepository _repository;

		public EmployeeController(IEmployeeRepository repository)
		{
			_repository = repository;
		}

		[HttpPost]
		public IActionResult Create([FromBody]CreateEmployeeModel employee)
		{
			return ExecuteResult(() =>
			{
				_repository.Insert(new Employee() { FullName = employee.FullName, Position = employee.Position});
				return true;
			});
		}

		[HttpGet]
		public IActionResult Get()
		{
			return ExecuteResult(() => _repository.Select());
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			return ExecuteResult(() => _repository.Select(id));
		}

		[HttpPut]
		public IActionResult Update([FromBody] Employee employee)
		{
			return ExecuteResult(() =>
			{
				_repository.Update(employee);
				return true;
			});
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			return ExecuteResult(() =>
			{
				_repository.Delete(id);
				return true;
			});
		}

		private IActionResult ExecuteResult(Func<object> func)
		{
			try
			{
				return Ok(func());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.GetBaseException().Message);
			}
		}
	}
}