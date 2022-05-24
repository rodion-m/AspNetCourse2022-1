using System.ComponentModel.DataAnnotations;

namespace Lesson04.RazorPagesApp.Models;

public class CategoryAddingModel
{
    public long Id { get; set; }
    public long ParentId { get; set; }

    [MinLength(5)] public string Name { get; set; } = "";
}