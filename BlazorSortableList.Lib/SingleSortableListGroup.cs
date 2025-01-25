namespace BlazorSortableList;

public class SingleSortableListGroup<T> : SortableListGroup<T>, ISortableListItemMover
{
    private readonly IList<T> _items;

    public ISortableListModel<T> Model { get; }

    public SingleSortableListGroup(string id, ISortableListModel<T> model)

    {
        AddModel(id, model);

        Model = model;
        _items = model.Items;
    }

    public virtual bool HandleRemove(string fromId, string toId, int oldIndex, int newIndex)
    {
        return false;
    }

    public virtual bool HandleUpdate(string id, int oldIndex, int newIndex)
    {
        SortList(oldIndex, newIndex);
        //refresh control
        return false;
    }

    private void SortList(int oldIndex, int newIndex)
    {
        var items = _items;
        var itemToMove = items[oldIndex];
        items.RemoveAt(oldIndex);

        if (newIndex < items.Count)
        {
            items.Insert(newIndex, itemToMove);
        }
        else
        {
            items.Add(itemToMove);
        }
    }
}
