using System;

namespace Projet_Csharp.Models;

public class Car
{
	public Guid Id { get; set; } = Guid.NewGuid();
	
	public string Brand { get; set; } = string.Empty;
	public string Model { get; set; } = string.Empty;
	public int Year { get; set; }
	public decimal PriceExclTax { get; set; }

	public string Color { get; set; } = string.Empty;
	public bool Traitor { get; set; }
	public List<Customer> Customers { get; set; } = new();

	public string DisplayName => $"{Brand} {Model} ({Year})";
	



	
}
