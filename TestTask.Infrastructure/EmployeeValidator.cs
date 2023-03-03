using FluentValidation;
using TestTask.Domain;

namespace TestTask.Infrastructure
{
	internal class EmployeeValidator : AbstractValidator<Employee>
	{
		public EmployeeValidator()
		{
			RuleFor(e => e.FullName)
				.NotEmpty()
				.WithMessage("The full name cannot be empty")
				.Matches("^([A-Z][a-z]*(-[A-Za-z][a-z]*)?\\s){2}[A-Z][a-z]*(-[A-Za-z][a-z]*)?$")
				.WithMessage("Invalid full name format");
			RuleFor(e => e.Position)
				.NotEmpty()
				.WithMessage("The position cannot be empty")
				.Matches("[A-Z0-9][a-z0-9\\s]*")
				.WithMessage("Invalid position format");
		}
	}
}
