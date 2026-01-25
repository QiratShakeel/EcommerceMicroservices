namespace Ecommerce.Catalog.Domain.Aggregates
{

    public class ProductInventory
    {
        //public int ProductId { get; private set; }
        public int StockQuantity { get; private set; }
        public int ReservedQuantity { get; private set; }
        public string? WarehouseLocation { get; private set; }
        public int AvailableStock => StockQuantity - ReservedQuantity;

        private ProductInventory() { } // REQUIRED for EF
        public ProductInventory(int stock, string? location=null)
        {
            if (stock < 0) throw new ArgumentException("Stock cannot be negative.");

            //ProductId = productId;
            StockQuantity = stock;
            ReservedQuantity = 0;
            WarehouseLocation = location;
        }

        public void AddStock(int amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive.");
            StockQuantity += amount;
        }

        public void ReduceStock(int amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive.");
            if (StockQuantity - amount < 0)
                throw new InvalidOperationException("Not enough stock.");

            StockQuantity -= amount;
        }

        public void ReserveStock(int amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive.");
            if (amount > AvailableStock)
                throw new InvalidOperationException("Not enough stock to reserve.");

            ReservedQuantity += amount;
        }

        public void ReleaseReserved(int amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive.");
            if (ReservedQuantity - amount < 0)
                throw new InvalidOperationException("Cannot release more than reserved.");

            ReservedQuantity -= amount;
        }
    }
}