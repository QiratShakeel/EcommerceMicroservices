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
        var form = new MultipartFormDataContent();

        form.Add(new StringContent("Test Laptop"), "Name");
        form.Add(new StringContent("SKU-TEST-001"), "SKU");
        form.Add(new StringContent("100"), "Price");
        form.Add(new StringContent("10"), "stock");
        form.Add(new StringContent("Test Description"), "Desc");

        var categoryId = Guid.NewGuid();
        form.Add(new StringContent(categoryId.ToString()), "CategoryIds");

        var fileBytes = Encoding.UTF8.GetBytes("fake image");

        var fileContent = new ByteArrayContent(fileBytes);
        fileContent.Headers.ContentType =
            new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

        form.Add(fileContent, "Images", "test.jpg");

        var response = await _client.PostAsync("/api/products", form);

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
