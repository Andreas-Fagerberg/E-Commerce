namespace E_commerce_Databaser_i_ett_sammanhang;

// A user can have multiple addresses. (one-to-many)
public class Address
{
    [Key]
    public Guid AddressId { get; set; }

    [Required]
    public Guid UserId { get; set; } // FK

    [Required]
    [MaxLength(100)]
    public string Street { get; set; }

    [Required]
    [MaxLength(50)]
    public string City { get; set; }

    [Required]
    [MaxLength(50)]
    public string Region { get; set; }

    [Required]
    [MaxLength(20)]
    public string PostalCode { get; set; }

    [Required]
    [MaxLength(50)]
    public string Country { get; set; }

    [Required]
    public User User { get; set; } // Navigation property



}