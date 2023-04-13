using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace GreatShop.WebApi.IntegrationTest;

public class Faker : Bogus.Faker
{
    public Faker() : base("ru")
    {
    }
}

public class IntegrationTests : IClassFixture<CustomWebApplicationFactory>, IClassFixture<Faker>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly ITestOutputHelper _output;
    private readonly Faker _faker;

    public IntegrationTests(CustomWebApplicationFactory factory, ITestOutputHelper output, Faker faker)
    {
        _factory = factory;
        _output = output;
        _faker = faker;
    }

    [Fact]
    public async Task Ping_endpoint_returns_pong()
    {
        //примерно 600 мс на первичное создание клиента
        HttpClient client = _factory.CreateClient(); //внутри процесса (Kestrel не запускается)
        // Здесь может быть создание ShopClient
        var response = await client.GetAsync($"/ping?name={_faker.Name.FullName()}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("pong", content);
    }
}