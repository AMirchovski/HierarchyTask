namespace HierarchyRefactored.Assets;

public class Item
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    public Item(int id, string name)
    {
        Id = id;
        Name = name;
    }
}