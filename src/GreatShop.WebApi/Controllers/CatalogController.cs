using System.ComponentModel.DataAnnotations;
using GreatShop.Domain;
using GreatShop.Domain.Services;
using GreatShop.HttpModels.Responses.Catalog.V1;
using GreatShop.HttpModels.Responses.Common;
using GreatShop.WebApi.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreatShop.WebApi.Controllers;

[ApiController]
[Route("catalog")]
public class CatalogController : ControllerBase //Adapter
{
    private readonly CatalogService _catalogService;
    private readonly HttpModelsMapper _mapper;

    public CatalogController(
        CatalogService catalogService, HttpModelsMapper mapper)
    {
        _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet("v1/get_product")]
    public async Task<IActionResult> GetProductV1([Required] Guid productId)
    {
        var product = await _catalogService.GetProduct(productId);
        var productModel = _mapper.MapProductModelV1(product);
        return Ok(productModel);
    }

    [HttpGet("v1/get_products")]
    public async Task<ActionResult<GetProductsResponseV1>> GetProductsV1([Required] Guid categoryId)
    {
        var products = await _catalogService.GetProducts(categoryId);
        return new GetProductsResponseV1(products.Select(_mapper.MapProductModelV1));
    }
    
    [HttpGet("v1/get_all_products")]
    public async Task<GetProductsResponseV1> GetAllProductsV1()
    {
        var products = await _catalogService.GetAllProducts();
        return new GetProductsResponseV1(products.Select(_mapper.MapProductModelV1));
    }
    
    [Authorize(Roles = nameof(Role.Admin))]
    [HttpGet("v1/add_product")]
    public async Task<ActionResult<Guid>> AddProductV1(ProductModelV1 request)
    {
        var product = await _catalogService.AddProduct(
            request.Name, request.CategoryId, request.Price, request.ImageUri);
        return product.Id;
    }

}