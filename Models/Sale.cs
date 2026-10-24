using System;

namespace Projet_Csharp.Models;

public class Sale
{
	public Guid Id { get; set; } = Guid.NewGuid();

	public Guid CarId { get; set; }
	public Car Car { get; set; } = null!;
	public DateTime SaleDate { get; set; }
	public decimal PriceExclTax { get; set; }
}
