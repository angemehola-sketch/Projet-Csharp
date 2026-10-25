using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Csharp;

[Table("Customers")]
public class Customer
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string LastName { get; set; } = "";

    [Required]
    public string FirstName { get; set; } = "";

    public DateTime BirthDate { get; set; }

    public string Phone { get; set; } = "";

    public string Email { get; set; } = "";

    // Voitures possédées par le client
    public ICollection<Ownership> Ownerships { get; set; } = new List<Ownership>();
}