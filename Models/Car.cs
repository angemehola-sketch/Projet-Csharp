using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Csharp;

[Table("Cars")]
public class Car
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Brand { get; set; } = "";

    [Required]
    public string Model { get; set; } = "";

    public int Year { get; set; }

    public decimal PriceExclTax { get; set; }

    public string Color { get; set; } = "";

    public bool IsSold { get; set; }

    public ICollection<Ownership> Ownerships { get; set; } = new List<Ownership>();
}