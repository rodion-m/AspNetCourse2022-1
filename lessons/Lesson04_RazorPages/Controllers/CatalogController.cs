using Lesson04_HttpModels;
using Lesson04_RazorPages.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lesson04_RazorPages.Controllers;

public class CatalogController : Controller
{
    private readonly ILogger<CatalogController> _logger;
    private readonly dynamic AllCategories = new object();

    public CatalogController(ILogger<CatalogController> logger)
    {
        _logger = logger;
    }

    public IActionResult ProductsList(int categoryId, string filterData)
    {
        return View(new[]
        {
            new Product(Guid.NewGuid(), "Чистый код", 1000m),
            new Product(Guid.NewGuid(), "Элегантные объекты", 1200m),
            new Product(Guid.NewGuid(), "Чистая архитектура", 1500m)
        });
    }

    public IActionResult CategoryList(int parentId)
    {
        var cats = AllCategories.Where(new Func<dynamic, bool>(it => it.parentId == parentId));
        return View(cats);
    }

    public IActionResult CategoryList()
    {
        Category[] categories = { new Category(1, "Духи"), new Category(2, "Книги") };
        return View(categories);
    }


    public IActionResult CatalogEditor([FromForm] CategoryAddingModel model)
    {
        if (HttpContext.Request.Method == "POST")
            // var id = HttpContext.Request.Form["id"].ToString();
            // var name = HttpContext.Request.Form["name"].ToString();
            throw new Exception(ModelState.IsValid.ToString());
        //categories.AddCategory(id, name);

        return View();
    }
}