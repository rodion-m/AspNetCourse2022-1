using System.ComponentModel.DataAnnotations;

namespace Lesson4_RazorPages.Models;

public class CategoryAddingModel
{
    public int Id { get; set; }

    [MinLength(5)] public string Name { get; set; }
}