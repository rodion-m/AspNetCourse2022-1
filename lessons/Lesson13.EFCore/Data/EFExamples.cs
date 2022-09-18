using Lesson14.Models;
using Microsoft.EntityFrameworkCore;

namespace Lesson13.EFCore.Data;

public class EFExamples
{
    public async void Example()
    {
        using var db = new AppDbContext(new DbContextOptions<AppDbContext>());
        Console.WriteLine("Inserting a new blog");
        db.Add(new Order { Phone = "+799912345678" });
        await db.SaveChangesAsync();

        // Read
        var blog = await db.Orders
            .OrderBy(b => b.Id)
            .FirstAsync();

        // Update
        blog.Phone = "https://devblogs.microsoft.com/dotnet";
        await db.SaveChangesAsync();

        // Delete
        Console.WriteLine("Delete the blog");
        db.Remove(blog);
        await db.SaveChangesAsync();
    }
}