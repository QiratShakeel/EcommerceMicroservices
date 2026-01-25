using AutoMapper;
using BuildingBlocks.Shared.Exceptions;
using BuildingBlocks.Shared.Infrastructure;
using Ecommerce.Catalog.Application.Dto;
using Ecommerce.Catalog.Application.Interfaces;
using Ecommerce.Catalog.Application.Queries;
using Ecommerce.Catalog.Domain.Aggregates;
using Moq;

namespace Catalog.Application.Tests.Queries
{
    public class GetProductByIdHandlerTests
    {
        private readonly Mock<IProductRepository> _repoMock;
        private readonly IMapper _mapper;

        public GetProductByIdHandlerTests()
        {
            _repoMock = new Mock<IProductRepository>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDto>().ForMember(dest=>dest.Price, opt=>opt.MapFrom(src=>src.Price.Amount)));
            _mapper = config.CreateMapper();
        }
        [Fact]
        public async Task Handle_ValidCommand_ShouldReturnSuccess()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product(
            name: "Test Product",
            sku: "SKU-123",
            price: new Money(100),
            description: "Test Desc"
            );
            var query = new GetProductByIdQuery(productId);
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(product);
            var handler = new GetProductByIdHandler(_repoMock.Object, _mapper);
            //Act
            var result = await handler.Handle(query, CancellationToken.None);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.SKU, result.SKU);
            Assert.Equal(product.Price.Amount, result.Price);
            Assert.Equal(product.Description, result.Description);

            _repoMock.Verify(r => r.GetByIdAsync(productId, It.IsAny<CancellationToken>()), Times.Once);

        }
        [Fact]
        public async Task Handle_ProductNotFound_ShouldThrowException()
        {
            // Arrange
            var query = new GetProductByIdQuery(Guid.NewGuid());

            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync((Product)null);

            var handler = new GetProductByIdHandler(_repoMock.Object, _mapper);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                handler.Handle(query, CancellationToken.None)
            );
        }


    }
}