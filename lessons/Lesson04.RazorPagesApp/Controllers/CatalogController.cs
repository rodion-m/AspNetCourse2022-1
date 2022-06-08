using System.Collections.Concurrent;
using Lesson04.HttpModels;
using Lesson04.RazorPagesApp.Models;
using Microsoft.AspNetCore.Mvc;

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
        Product[] products = new[]
        {
            new Product(Guid.NewGuid(), "Чистый код", 1000m),
            new Product(Guid.NewGuid(), "Элегантные объекты", 1200m),
            new Product(Guid.NewGuid(), "Чистая архитектура", 1500m)
        };
        return View(products);
    }

    public IActionResult CategoryList(int? parentId)
    {
        if (parentId is null)
        {
            return View(_categories      
                .OrderBy(it => it.Id).ToList()
            );
        }

        var parent = _categories.FirstOrDefault(it => it.Id == parentId);
        if (parent is not null)
        {
            ViewData["Title"] = $"Список категорий в {parent.Name}";
        }
        else
        {
            ViewData["Title"] = "Такой категории не существует";
        }

        var cats = _categories
            .Where(it => it.ParentId == parentId)
            .OrderBy(it => it.Id);
            
        return View(cats.ToList());
    }

    [HttpGet]
    public IActionResult CategoryAdding()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult CategoryAdding([FromForm] CategoryModel model)
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
    
    [HttpGet]
    public IActionResult CategoryEditing(int categoryId)
    {
        var maybeCategory = _categories.FirstOrDefault(it => it.Id == categoryId);
        return View(maybeCategory is { } cat 
            ? new CategoryModel(cat.Id, cat.ParentId, cat.Name) 
            : null);
    }
    
    [HttpPost]
    public IActionResult CategoryEditing([FromQuery] int categoryId, [FromForm] CategoryModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var cat = _categories.First(it => it.Id == categoryId);
        cat.ParentId = model.ParentId;
        cat.Name = model.Name;
        ViewData["Message"] = "Категория изменена";
        return View(model);
    }
}