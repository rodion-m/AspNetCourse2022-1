using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Lesson04.RazorPagesApp.Models;

public class CategoryModel
{
    public CategoryModel()
    {
    }
    public CategoryModel(long id, long parentId, string name)
    {
        Id = id;
        ParentId = parentId;
        Name = name;
    }

    [Range(0, int.MaxValue)]
    public long Id { get; set; }
    
    [Range(0, int.MaxValue)]
    public long ParentId { get; set; }

    [Display(Name = "Название")]
    [Required]
    public string Name { get; set; } = "";
}