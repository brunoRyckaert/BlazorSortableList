using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorSortableList
{
    public partial class SortableList<T>
    {
        private ISortableListItemMover? _sortableListHandler;

        private ISortableListSelection? _sortableListSelection;

        private DotNetObjectReference<SortableList<T>>? _selfReference;

        private string? _cssForSelection;

        private string? _multiDragKey = string.Empty;

        private bool _avoidImplicitDeselect;

        private double _swapThreshold = 1.0;

        private bool _fallbackOnBody = false;

        [Parameter]
        public bool DefaultSort { get; set; }

        [Parameter]
        public string? Filter { get; set; }

        [Parameter]
        public bool ForceFallback { get; set; } = true;

        [Parameter]
        public string Group { get; set; } = Guid.NewGuid().ToString();

        [Parameter]
        public ISortableListGroup<T>? GroupModel { get; set; }

        [Parameter]
        public string? Handle { get; set; }

        [Parameter]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Parameter]
        [AllowNull]
        public IList<T> Items { get; set; }

        [Parameter]
        public EventCallback<(int oldIndex, int newIndex)> OnRemove { get; set; }

        [Parameter]
        public EventCallback<(int oldIndex, int newIndex)> OnUpdate { get; set; }

        [Parameter]
        public string? Pull { get; set; }

        [Parameter]
        public bool Put { get; set; } = true;

        [Parameter]
        public bool Sort { get; set; } = true;
        [Parameter]
        public string? Style { get; set; }
        
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object>? Attributes { get; set; }

        [Parameter]
        public RenderFragment<T>? SortableItemTemplate { get; set; }

        public void Dispose() => _selfReference?.Dispose();

        [JSInvokable]
        public void OnDeselectJS(string fromId, int index)
        {
            if (_sortableListSelection == null) 
                return;
            
            if (_sortableListSelection.HandleDeselect(fromId, index))
            {
                StateHasChanged();
            }
        }

        [JSInvokable]
        public void OnRemoveJS(int oldIndex, int newIndex, string fromId, string toId)
        {
            if (_sortableListHandler?.HandleRemove(fromId, toId, oldIndex, newIndex) ?? false)
                StateHasChanged();
            else
                OnRemove.InvokeAsync((oldIndex, newIndex));
        }

        [JSInvokable]
        public void OnSelectJS(string fromId, int index)
        {
            if (_sortableListSelection?.HandleSelect(fromId, index) ?? false)
                StateHasChanged();
        }

        [JSInvokable]
        public void OnUpdateJS(int oldIndex, int newIndex, string fromId)
        {
            if (_sortableListHandler?.HandleUpdate(fromId, oldIndex, newIndex) ?? false)
            {
                OnUpdate.InvokeAsync((oldIndex, newIndex));
                return;
            }

            if (OnUpdate.HasDelegate)
            {
                if (DefaultSort)
                {
                    throw new ArgumentException(
                        "It must be defined as either {nameof(OnUpdate)} or {nameof(DefaultSort)}, but not both together.");
                }

                // invoke the OnUpdate event passing in the oldIndex and the newIndex
                OnUpdate.InvokeAsync((oldIndex, newIndex));
                return;
            }
            
            if (DefaultSort)
            {
                OnUpdate.InvokeAsync((oldIndex, newIndex));
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
                return;

            _selfReference = DotNetObjectReference.Create(this);
            var module = await JS.InvokeAsync<IJSObjectReference>("import",
                "./_content/BlazorSortableList-fork-attribute_splatting/SortableList.razor.js");

             await module.InvokeAsync<string>(
                "init",
                Id,
                Group,
                Pull,
                Put,
                Sort,
                Handle,
                Filter,
                _selfReference,
                ForceFallback,
                _cssForSelection,
                _multiDragKey,
                _avoidImplicitDeselect,
                _fallbackOnBody,
                _swapThreshold
            );
        }

        /// <summary>
        /// Method invoked when the component has received parameters from its parent in
        /// the render tree, and the incoming values have been assigned to properties.
        /// </summary>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> representing any asynchronous operation.</returns>
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            _sortableListHandler = GroupModel as ISortableListItemMover;
            _sortableListSelection = GroupModel as ISortableListSelection;

            var model = GroupModel?.GetModel(Id);
            if (model == null)
                return;
            
            Group = model.Group;
            Items = model.Items;
            var settings = model.Settings;

            if (settings.CloneMode)
                Pull = "clone";

            Put = settings.AllowDrop;
            Sort = settings.AllowReorder;
            
            if (string.IsNullOrEmpty(Handle))
                Handle = settings.CssForDragHandle;

            Filter = settings.CssForDisabledItem;
            ForceFallback = !settings.AllowHtml5DragAndDrop;
            if (settings.MultiSelection)
            {
                _cssForSelection = string.IsNullOrEmpty(settings.CssForSelection)
                    ? "sortable-selected"
                    : settings.CssForSelection;
                _multiDragKey = settings.AddToSelectionKey;
                _avoidImplicitDeselect = settings.AvoidImplicitDeselect;

                _multiDragKey ??= String.Empty;
            }

            // actual for the nested lists
            _fallbackOnBody = settings.FallbackOnBody;
            _swapThreshold = settings.SwapThreshold;
        }

        private void SortList(int oldIndex, int newIndex)
        {
            StateHasChanged();
        }
    }
}
