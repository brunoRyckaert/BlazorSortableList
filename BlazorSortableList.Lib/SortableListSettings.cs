namespace BlazorSortableList;

public class SortableListSettings
{
    /// <summary>
    /// Gets or sets a value indicating whether this list allows dropping from another list.
    /// Note: Similar to the Put parameter
    /// </summary>
    /// <value><c>true</c> if [allow drop]; otherwise, <c>false</c>.</value>
    public bool AllowDrop { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether this list used HTML5 drag and drop mode.
    /// Note: Similar to the inverted ForceFallback parameter
    /// </summary>
    /// <value><c>true</c> if [allow HTML5 drag and drop]; otherwise, <c>false</c>.</value>
    public bool AllowHtml5DragAndDrop { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether the list allows reordering of items.
    /// Note: Similar to the Sort parameter
    /// </summary>
    /// <value><c>true</c> if [allow reorder]; otherwise, <c>false</c>.</value>
    public bool AllowReorder { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether the list is in clone mode.
    /// Note: Similar to the Pull parameter
    /// </summary>
    /// <value><c>true</c> if [clone mode]; otherwise, <c>false</c>.</value>
    public bool CloneMode { get; set; } = false;

    /// <summary>
    /// Gets or sets the CSS class name for the disabled list item
    /// Note: Similar to the Filter parameter
    /// </summary>
    /// <value>The CSS for disabled item.</value>
    public string CssForDisabledItem { get; set; } = String.Empty;

    /// <summary>
    /// Gets or sets the CSS class name to display the drag handle of the list item.
    /// Note: Similar to the Handle parameter
    /// </summary>
    /// <value>The CSS for drag handle with leading point</value>
    public string CssForDragHandle { get; set; } = String.Empty;

    /// <summary>
    /// Gets or sets the CSS class name for the selected element.
    /// </summary>
    /// <value>The CSS for selection.</value>
    public string? CssForSelection { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether multiple selection is allowed.
    /// </summary>
    /// <value><c>true</c> if [multi selection]; otherwise, <c>false</c>.</value>
    public bool MultiSelection { get; set; }

    /// <summary>
    /// Gets or sets the multi drag key. Case is ignored
    /// alt for example
    /// </summary>
    /// <value>The multi drag key.</value>
    public string? AddToSelectionKey { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to avoid implicit deselection.
    /// By default, (un-moved) items are deselected by clicking outside.
    /// 
    /// </summary>
    /// <value><c>true</c> if you don't want to deselect items on outside click; otherwise, <c>false</c>.</value>
    public bool AvoidImplicitDeselect { get; set; }

    /// <summary>
    /// Gets or sets the swap threshold.
    /// Threshold of the swap zone.
    /// </summary>
    /// <value>The swap threshold.</value>
    public double SwapThreshold { get; set; } = 1.0;

    /// <summary>
    /// Gets or sets a value indicating whether appends the cloned DOM Element into the Document's Body allowed
    /// </summary>
    /// <value><c>true</c> if [fallback on body]; otherwise, <c>false</c>.</value>
    public bool FallbackOnBody { get; set; }= false;
}
