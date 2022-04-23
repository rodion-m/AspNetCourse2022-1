using System.ComponentModel.DataAnnotations;
using GreatShop.Domain.Services;
using GreatShop.HttpModels.Responses.Catalog.V1;
using GreatShop.HttpModels.Responses.Common;
using GreatShop.WebApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace GreatShop.WebApi.Controllers;

[ApiController]
[Route("catalog")]
public class CatalogController : ControllerBase
{
    private readonly CatalogService _catalogService;
    private readonly HttpModelsMapper _mapper;

    public CatalogController(CatalogService catalogService, HttpModelsMapper mapper)
    {
        _catalogService = catalogService;
        _mapper = mapper;
    }

    [HttpGet("v1/get_product")]
    public async Task<IActionResult> GetProductV1([Required] Guid productId)
    {
        var product = await _catalogService.GetProduct(productId);
        return Ok(_mapper.MapProductModelV1(product));
    }

    [HttpGet("v1/get_products")]
    public async Task<GetProductsResponseV1> GetProductsV1([Required] Guid categoryId)
    {
        var products = await _catalogService.GetProducts(categoryId);
        return new GetProductsResponseV1(products.Select(_mapper.MapProductModelV1));
    }
}