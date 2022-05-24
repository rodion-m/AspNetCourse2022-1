using System.Collections.Concurrent;
using Lesson04.HttpModels;
using Lesson04.RazorPagesApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Lesson04.RazorPagesApp.Controllers;

public class CatalogController : Controller
{
    public string? Message { get; set; }
    
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

    public IActionResult CategoryList(int? parentId)
    {
        if (parentId is null)
        {
            return View(_categories);
        }

        var cats = _categories.Where(it => it.ParentId == parentId);
        return View(cats.ToList());
    }

    [HttpGet]
    public IActionResult CategoryAdding()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult CategoryAdding([FromForm] CategoryAddingModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Message"] = "Некорректные данные. Исправьте ошибки и попробуйте еще раз.";
            return View(model);
        }
        _categories.Add(new Category(model.Id, model.ParentId, model.Name));
        ViewData["Message"] = "Категория добавлена";
        return View();
    }
}