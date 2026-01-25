using BuildingBlocks.Shared.Infrastructure;

namespace BuildingBlocks.Shared.Tests.Infrastructure
{
    public class MoneyTests
    {
        [Fact]
        public void Money_WithNegAmmount_ShouldThrowError()
        {
            Assert.Throws<ArgumentException>(() => new Money(-2));
        }
        [Fact]
        public void Money_WithValidAmmount_ShouldSuceed()
        {
            var money = new Money(23);
            Assert.Equal(23, money.Amount);
        }
    }
}