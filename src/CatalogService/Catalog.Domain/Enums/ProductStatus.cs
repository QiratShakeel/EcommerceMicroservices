using System;

namespace Ecommerce.Catalog.Domain.Enums
{
    public enum ProductStatus   
    {
        Draft = 0,         // Product is being created, not visible
        Active = 1,        // Fully visible and available for purchase
        Inactive = 2,      // Not visible, but might return later
        Discontinued = 3   // No longer sold, possibly visible for history but not purchasable
    }
}

