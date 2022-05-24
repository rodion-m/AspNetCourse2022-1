namespace Lesson04.HttpModels;

public class Category
{
    public Category(long CategoryId, long ParentId, string Name)
    {
        this.CategoryId = CategoryId;
        this.ParentId = ParentId;
        this.Name = Name;
    }

    public long CategoryId { get; set; }
    public long ParentId { get; set; }
    public string Name { get; set; }
}