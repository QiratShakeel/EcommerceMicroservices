using AutoMapper;
using BuildingBlocks.Shared.Infrastructure;
using Ecommerce.Catalog.Application.Commands;
using Ecommerce.Catalog.Application.Interfaces;
using Ecommerce.Catalog.Domain.Aggregates;
using Ecommerce.Catalog.Domain.ValueObjects;
using Moq;
using Xunit;

namespace Catalog.Application.Tests.Commands
{
    public class CreateProductHandlerTests
    {
        private readonly Mock<IProductCommandRepository> _repoMock;
        private readonly Mock<IProductQueries> _queriesMock;
        private readonly Mock<IFileService> _fileServiceMock;
        private readonly IMapper _mapper;

        public CreateProductHandlerTests()
        {
            _repoMock = new Mock<IProductCommandRepository>();
            _queriesMock = new Mock<IProductQueries>();
            _fileServiceMock = new Mock<IFileService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateProductCommand, Product>()
                   .ForCtorParam("Name", opt => opt.MapFrom(c => c.Name))
                   .ForCtorParam("SKU", opt => opt.MapFrom(c => c.SKU))
                   .ForCtorParam("Price", opt => opt.MapFrom(c => new Money(c.Price)))
                   .ForCtorParam("Description", opt => opt.MapFrom(c => c.Desc));
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldReturnSuccess()
        {
            // Arrange
            var cmd = new CreateProductCommand(
                "Laptop",
                "SKU-001",
                100m,
                10,
                "Some description",
                new List<Guid> { Guid.NewGuid() },
                null
            );

            _queriesMock
                .Setup(x => x.IsSkuUniqueAsync(cmd.SKU, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _repoMock
                .Setup(x => x.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var handler = new CreateProductHandler(
                _repoMock.Object,
                _mapper,
                _fileServiceMock.Object,
                _queriesMock.Object);

            // Act
            var result = await handler.Handle(cmd, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);

            _repoMock.Verify(
                x => x.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_DuplicateSku_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var cmd = new CreateProductCommand(
                "Laptop",
                "SKU-001",
                100m,
                10,
                "Description",                
                null,
                null);

            _queriesMock
                .Setup(x => x.IsSkuUniqueAsync(cmd.SKU, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var handler = new CreateProductHandler(
                _repoMock.Object,
                _mapper,
                _fileServiceMock.Object,
                _queriesMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(cmd, CancellationToken.None));
        }
    }
}
