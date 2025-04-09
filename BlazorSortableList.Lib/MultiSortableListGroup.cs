namespace BlazorSortableList;


/// <summary>
/// Class TwoSortableListGroup.
/// Implements the <see cref="BlazorSortableList.SortableListGroup{T}" />
/// Default implementation sorts list1 and list2, and moves items from list1 to list2 and vice versa.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="BlazorSortableList.SortableListGroup{T}" />
public class MultiSortableListGroup<T> : SortableListGroup<T>, ISortableListItemMover
{
    private readonly Action _refreshComponent;
 

    protected MultiSortableListGroup(Action refreshComponent)
    {
        _refreshComponent = refreshComponent;
    }

    public virtual bool HandleRemove(string fromId, string toId, int oldIndex, int newIndex)
    {
        var items1 = GetModel(fromId)?.Items;
        var items2 = GetModel(toId)?.Items;
        
        if (items1 == null || items2 == null) 
            return false;
        
        ListMoveItem(oldIndex, newIndex, items1, items2);
            
        _refreshComponent();

        return false;
    }

    public virtual bool HandleUpdate(string id, int oldIndex, int newIndex)
    {
        var items = GetModel(id)?.Items;
        if (items != null)
        {
            ListArrangeItems(oldIndex, newIndex, items);
        }

        _refreshComponent();

        return false;
    }

    /// <summary>
    /// Move items between lists
    /// </summary>
    /// <param name="oldIndex">The old index.</param>
    /// <param name="newIndex">The new index.</param>
    /// <param name="items1">The items1.</param>
    /// <param name="items2">The items2.</param>
    protected virtual void ListMoveItem(int oldIndex, int newIndex, IList<T> items1, IList<T> items2)
    {
        // get the item at the old index in list 1
        var item = items1[oldIndex];
        if (newIndex < items2.Count)
        {
            // add it to the new index in list 2
            items2.Insert(newIndex, item);
        }
        else
        {
            items2.Add(item);
        }

        // remove the item from the old index in list 1
        items1.RemoveAt(oldIndex);
    }

    /// <summary>
    /// Arrange items within the list.
    /// </summary>
    /// <param name="oldIndex">The old index.</param>
    /// <param name="newIndex">The new index.</param>
    /// <param name="items">The items.</param>
    protected virtual void ListArrangeItems(int oldIndex, int newIndex, IList<T> items)
    {
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
