namespace BlazorSortableList;

public interface ISortableListModel<T>
{
    string Group { get; }

    public SortableListSettings Settings { get; }

    IList<T> Items { get; }
}