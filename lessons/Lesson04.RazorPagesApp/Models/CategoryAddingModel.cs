using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Lesson04.RazorPagesApp.Models;

public class CategoryAddingModel
{
    [FromForm(Name = "id")]
    [Range(0, long.MaxValue)]
    public long Id { get; set; }
    
    [FromForm(Name = "parent_id")]
    [Range(0, long.MaxValue)]
    public long ParentId { get; set; }

    [FromForm(Name = "name")]
    [Required]
    public string Name { get; set; } = "";
}