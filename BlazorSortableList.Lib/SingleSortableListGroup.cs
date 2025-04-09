namespace BlazorSortableList;

public class SingleSortableListGroup<T> : SortableListGroup<T>, ISortableListItemMover
{
    private readonly IList<T> _items;
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
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
        var itemToMove = _items[oldIndex];
        _items.RemoveAt(oldIndex);

        if (newIndex < _items.Count)
        {
            _items.Insert(newIndex, itemToMove);
        }
        else
        {
            _items.Add(itemToMove);
        }
    }
}
