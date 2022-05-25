namespace Lesson04.HttpModels;

public class Category
{
    public Category(long id, long ParentId, string Name)
    {
        this.Id = id;
        this.ParentId = ParentId;
        this.Name = Name;
    }

    public long Id { get; set; }
    public long ParentId { get; set; }
    public string Name { get; set; }
}