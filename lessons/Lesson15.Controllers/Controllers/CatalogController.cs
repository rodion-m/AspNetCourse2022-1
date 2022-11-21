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
        _service = service;
    }

    [HttpPost("find_category")]
    //public async Task<ActionResult<IEnumerable<Category>>> FindCategory(CategoryFilterModel model)
    public ActionResult<Category> FindCategory(CategoryFilterModel model)
    {
        var cat = _service.FindCategory(model.Text);
        if (cat == null) return NotFound();

        return new ObjectResult(cat)
        {
            DeclaredType = typeof(Category),
            StatusCode = StatusCodes.Status200OK
        };
        //IEnumerable<Category> cats = _categories;
        //return cats;
    }

    public Category? FindCategory(string text)
    {
        var cat = _service.FindCategory(text);
        return cat;
    }

    public ActionResult<Category> FindCategory1(string text)
    {
        var cat = _service.FindCategory(text);
        if (cat == null) return NotFound();
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
}