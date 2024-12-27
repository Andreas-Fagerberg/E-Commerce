using System.ComponentModel.DataAnnotations;

namespace E_commerce_Databaser_i_ett_sammanhang;

public class Address
{
    [Key]
    public Guid AddressId { get; set; }

    [Required]
    public Guid UserId { get; set; } // FK

    [Required]
    [MaxLength(100)]
    public required string Street { get; set; }

    [Required]
    [MaxLength(50)]
    public required string City { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Region { get; set; }

    [Required]
    [MaxLength(20)]
    public required string PostalCode { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Country { get; set; }

    // Navigation property
    public required User User { get; set; }
}
