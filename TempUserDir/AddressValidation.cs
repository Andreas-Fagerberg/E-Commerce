
namespace E_commerce_Databaser_i_ett_sammanhang;

public class AddressValidation
{
    /// <summary>
    /// Validates user address data, including street, 
    /// city, region, postal code and country.
    /// </summary>
    public static void ValidateAddress(RegisterAddressDTO dto)
    {
        // CheckForValidUser should be handled in the Command layer.

        if (string.IsNullOrWhiteSpace(dto.Street) || dto.Street.Length > 100)
        {
            throw new ArgumentException("Street cannot be empty (max 100 characters).");
        }

        if (string.IsNullOrWhiteSpace(dto.City) || dto.City.Length > 50)
        {
            throw new ArgumentException("City cannot be empty (max 50 characters).");
        }

        if (string.IsNullOrWhiteSpace(dto.Region) || dto.Region.Length > 50)
        {
            throw new ArgumentException("Region cannot be empty (max 50 characters).");
        }

        if (string.IsNullOrWhiteSpace(dto.PostalCode) || dto.PostalCode.Length > 25)
        {
            throw new ArgumentException("PostalCode cannot be empty (max 25 characters).");
        }

        if (string.IsNullOrWhiteSpace(dto.Country) || dto.Country.Length > 50)
        {
            throw new ArgumentException("Country cannot be empty (max 50 characters).");
        }

        ValidatePostalCode(dto.PostalCode, dto.Country);
    }




    #region Helper Methods

    /// <summary>
    /// Validates a postal code based on its general format and country-specific rules, 
    /// falling back to generic validation for unsupported countries.
    /// </summary>
    public static void ValidatePostalCode(string postalCode, string? country = null)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
        {
            throw new ArgumentException("Postal Code cannot be empty.");
        }

        // Normalize the country input
        country = country?.Trim();

        // Country name to ISO code mapping
        var countryMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Germany", "DE" },
            { "France", "FR" },
            { "United Kingdom", "UK" },
            { "United States", "US" },
            { "Canada", "CA" },
            { "Australia", "AU" },
            { "Sweden", "SE" },
            { "Norway", "NO" },
            { "Switzerland", "CH" },
            { "DE", "DE" },
            { "FR", "FR" },
            { "UK", "UK" },
            { "US", "US" },
            { "CA", "CA" },
            { "AU", "AU" },
            { "SE", "SE" },
            { "NO", "NO" },
            { "CH", "CH" }
        };

        // Map country name to ISO code
        if (!string.IsNullOrEmpty(country) && countryMapping.TryGetValue(country, out var isoCode))
        {
            country = isoCode;
        }
        else if (!string.IsNullOrEmpty(country))
        {
            Console.WriteLine($"Warning: Unsupported country '{country}'. Falling back to general validation.");
            country = null; // Clear country to trigger general validation.
        }

        // Country-specific postal code patterns
        var patterns = new Dictionary<string, string>
        {
            { "DE", @"^\d{5}$" },
            { "FR", @"^\d{5}$" },
            { "UK", @"^[A-Z]{1,2}\d[A-Z\d]? \d[A-Z]{2}$" },
            { "US", @"^\d{5}(-\d{4})?$" },
            { "CA", @"^[A-Za-z]\d[A-Za-z] ?\d[A-Za-z]\d$" },
            { "AU", @"^\d{4}$" },
            { "SE", @"^\d{3}\s?\d{2}$" },
            { "NO", @"^\d{4}$" },
            { "CH", @"^\d{4}$" }
        };

        // Perform country-specific validation if the country is supported
        if (!string.IsNullOrEmpty(country) && patterns.TryGetValue(country, out var pattern))
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(postalCode, pattern))
            {
                throw new ArgumentException($"Invalid postal code format for {country}.");
            }
        }
        else
        {
            // General validation for unsupported countries
            if (!System.Text.RegularExpressions.Regex.IsMatch(postalCode, @"^[a-zA-Z0-9\s\-]+$"))
            {
                throw new ArgumentException("Postal Code contains invalid characters.");
            }
            Console.WriteLine($"Notice: General validation applied for postal code '{postalCode}' in unsupported country.");
        }
    }

}





#endregion