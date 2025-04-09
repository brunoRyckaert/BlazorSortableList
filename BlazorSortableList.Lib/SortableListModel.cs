namespace BlazorSortableList;

public class SortableListModel<T> : ISortableListModel<T>
{
    public SortableListModel(IList<T> items)
    {
        Items = items;
    }

    public string Group { get; set; } = Guid.NewGuid().ToString();

    public SortableListSettings Settings { get; set; } = new();

    public IList<T> Items { get; }
}
