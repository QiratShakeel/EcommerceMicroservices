using Xunit;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using FluentAssertions;
using Ecommerce.Catalog.Application.Commands;
using System.Net;
using Ecommerce.Catalog.Domain.ValueObjects;

public class ProductsControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProductsControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateProduct_ShouldReturn201AndProductId()
    {
        // Arrange
        var command = new CreateProductCommand(
            "Test Laptop",
            "SKU-TEST-001",
            100m,
            "Test Description",
            new List<int> { 1 },
            new List<ProductImage> { new("https://img.com/a.jpg", "alt", ".jpg") }
        );

        var content = new StringContent(
            JsonSerializer.Serialize(command),
            Encoding.UTF8,
            "application/json"
        );

        // Act
        var response = await _client.PostAsync("/api/products", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        // Get Location header
        var location = response.Headers.Location;
        location.Should().NotBeNull();

        // Optional: GET the created product
        var getResponse = await _client.GetAsync(location);
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetAllProducts_ShouldReturnOk()
    {
        var response = await _client.GetAsync("/api/products");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
