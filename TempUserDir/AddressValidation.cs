
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace E_commerce_Databaser_i_ett_sammanhang;

public class AddressValidation
{

    public static void ValidateAddress(RegisterAddressDTO dto)
    {
        // CheckForValidUser should be handled in the Command layer.

        if (string.IsNullOrWhiteSpace(dto.Street) || dto.Street.Length > 100)
        {
            throw new ArgumentException("Street cannot be empty (max 100 characters).");
        }

        if (string.IsNullOrWhiteSpace(dto.City) || dto.City.Length > 50)
        {
            throw new ArgumentException("City cannot be empty (max 100 characters).");
        }

        if (string.IsNullOrWhiteSpace(dto.Region) || dto.Region.Length > 50)
        {
            throw new ArgumentException("Region cannot be empty (max 100 characters).");
        }

        if (string.IsNullOrWhiteSpace(dto.PostalCode) || dto.PostalCode.Length > 25)
        {
            throw new ArgumentException("PostalCode cannot be empty (max 100 characters).");
        }

        if (string.IsNullOrWhiteSpace(dto.Country) || dto.Country.Length > 50)
        {
            throw new ArgumentException("Country cannot be empty (max 100 characters).");
        }
    }


}