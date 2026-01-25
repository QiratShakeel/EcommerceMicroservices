//using Ecommerce.Catalog.Domain.Enums;
using System;

namespace BuildingBlocks.Shared.Infrastructure
{
    public record Money
    {
        public decimal Amount { get; init; }
        //public CurrencyCode Currency { get; init; } // Corrected to use Enum directly

        // Constructor now accepts the CurrencyCode enum instead of string
        public Money(decimal amount)
        {
            if (amount < 0) throw new ArgumentException("Amount cannot be negative.", nameof(amount));
            Amount = amount;
            //Currency = currencyCode != CurrencyCode.Unknown ? currencyCode : throw new ArgumentException("Currency code cannot be Unknown.", nameof(currencyCode));
        }

        // Method to add two Money instances together
        public Money Add(Money other)
        {
            // if (Currency != other.Currency)
            // {
            //     throw new InvalidOperationException("Cannot add money of different currencies.");
            // }

            return new Money(Amount + other.Amount);
        }

        // Override ToString for easier display
        //public override string ToString() => $"{Amount} {Currency}";
    }
}
