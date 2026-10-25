using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Csharp;

[Table("Ownerships")]
public class Ownership
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;

    public Guid CarId { get; set; }

    public Car Car { get; set; } = null!;

    public DateTime SaleDate { get; set; }
}