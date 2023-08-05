using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Lesson15.Controllers.Controllers;

[Route("catalog")]
[ApiController]
public class CatalogController : ControllerBase
{
    private readonly CatalogService _service;
    private Category[] _categories = { new("Книги"), new("Еда") };

    public CatalogController(CatalogService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpPost("find_category")]
    public ActionResult<Category> FindCategory(CategoryFilterModel model)
    {
        var cat = _service.FindCategory(model.Text);
        if (cat == null)
        {
            return NotFound();
        }

        return cat;
    }
    
    [HttpPost("find_category")]
    public async Task<ActionResult<Category>> FindCategory3(CategoryFilterModel model)
    {
        return ResponseOrNotFound(await _service.FindCategoryAsync(model.Text));
    }

    private ActionResult<T> ResponseOrNotFound<T>(T? reponse)
    {
        if (reponse == null)
        {
            return NotFound();
        }

        return reponse;
    }

    // IActionResult
    public IActionResult FindCategory(string text) //T
    {
        Category? cat = _service.FindCategory(text);
        if (cat is null)
        {
            return NotFound();
        }
        return Ok(cat);
    }
    
    // T
    public Category FindCategory2(string text)
    {
        Category? cat = _service.FindCategory(text);
        return cat;
    }

    [HttpGet("get_categories")] //Если не указано, то по умолчанию GET
    public ActionResult<Category> GetCategories()
    {
        return new ObjectResult(new Category(""))
        {
            DeclaredType = typeof(Category),
            StatusCode = StatusCodes.Status200OK
        };
        //Конечный адрес будет http://.../Catalog/
        //return _categories;
    }

    [HttpPost("add_category")]
    public void AddCategory(Category category)
    {
    }
}

public class Product

{
}

public class CategoryFilterModel
{
    [MinLength(1)] public string? Text { get; set; }
}

public class Category
{
    public Category(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}

public class CatalogService
{
    public Category? FindCategory(string text)
    {
        return null;
    }

    public async Task<Category?> FindCategoryAsync(string? text)
    {
        return new Category("");
    }
}