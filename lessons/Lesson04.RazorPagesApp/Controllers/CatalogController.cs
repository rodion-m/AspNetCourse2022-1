using System.Collections.Concurrent;
using Lesson04.HttpModels;
using Lesson04.RazorPagesApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lesson04.RazorPagesApp.Controllers;

public class CatalogController : Controller
{
    private static readonly ConcurrentBag<Category> _categories = new()
    {
        new Category(1, 0, "Продукты"), 
            new Category(10, 1, "Молоко"), 
            new Category(11, 1, "Кофе"),
            
        new Category(2, 0, "Электроника"),  
            new Category(20, 2, "Смартфоны"),
            new Category(21, 2, "Смарт-часы"),
    };

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
        var cats = _categories.Where(it => it.ParentId == parentId);
        return View(cats.ToList());
    }

    public IActionResult CategoryList()
    {
        return View(_categories.ToList());
    }


    [HttpGet]
    public IActionResult CatalogEditor()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult CatalogEditor([FromForm] CategoryAddingModel model)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem();
        }

        _categories.Add(new Category(model.Id, model.ParentId, model.Name));
        return View(model: "Категория добавлена");
    }
}