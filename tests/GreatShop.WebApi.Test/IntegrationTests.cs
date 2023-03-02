using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GreatShop.Data.Ef;
using GreatShop.Domain.Entities;
using GreatShop.Domain.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using Xunit;
using Xunit.Abstractions;

namespace GreatShop.WebApi.Test;


public class IntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public IntegrationTests(ITestOutputHelper output, CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Ping_endpoint_returns_pong()
    {
        HttpClient client = _factory.CreateClient();
        // Здесь может быть создание ShopClient
        var response = await client.GetAsync("/ping");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("pong", content);
    }
}