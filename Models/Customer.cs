using System;

namespace Projet_Csharp.Models;

public class Customer
{
	public Guid Id { get; set; } = Guid.NewGuid();
	
	public string LastName { get; set; } = string.Empty;
	public string FirstName { get; set; } = string.Empty;
	public DateOnly BirthDate { get; set; } 
	public string Phone { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public List<Car> Cars { get; set; } = new();

	public string FullName => $"{FirstName} {LastName}";

}
