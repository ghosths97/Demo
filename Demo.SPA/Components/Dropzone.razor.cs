using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Demo.SPA.Pages.Index;

namespace Demo.SPA.Components
{
    partial class Dropzone<TItem> 
    {
        /// <summary>
        /// Allows to pass a delegate which executes if something is dropped and decides if the item is accepted
        /// </summary>
        [Parameter] public Func<TItem, TItem, bool> Accepts { get; set; }

        /// <summary>
        /// Allows to pass a delegate which executes if something is dropped and decides if the item is accepted
        /// </summary>
        [Parameter] public Func<TItem, bool> AllowsDrag { get; set; }

        /// <summary>
        /// Raises a callback with the dropped item as parameter in case the item can not be dropped due to the given Accept Delegate
        /// </summary>
        [Parameter] public EventCallback<TItem> OnItemDropRejected { get; set; }

        /// <summary>
        /// Raises a callback with the dropped item as parameter
        /// </summary>
        [Parameter] public EventCallback<TItem> OnItemDrop { get; set; }

        [Parameter] public Func<TItem, TItem, bool> OnItemDroped { get; set; }

        /// <summary>
        /// If set to true, items will we be swapped/inserted instantly.
        /// </summary>
        [Parameter] public bool InstantReplace { get; set; }

        /// <summary>
        /// List of items for the dropzone
        /// </summary>
        [Parameter] public IList<TItem> Items { get; set; }

        /// <summary>
        /// Maximum Number of items which can be dropped in this dropzone. Defaults to null which means unlimited.
        /// </summary>
        [Parameter] public int? MaxItems { get; set; }

        /// <summary>
        /// Raises a callback with the dropped item as parameter in case the item can not be dropped due to item limit.
        /// </summary>
        [Parameter] public EventCallback<TItem> OnItemDropRejectedByMaxItemLimit { get; set; }

        [Parameter] public RenderFragment<TItem> ChildContent { get; set; }

        /// <summary>
        /// Specifies one or more classnames for the Dropzone element.
        /// </summary>
        [Parameter] public string Class { get; set; }

        /// <summary>
        /// Specifies the id for the Dropzone element.
        /// </summary>
        [Parameter] public string Id { get; set; }

        /// <summary>
        /// Specifies one or more classnames for the draggable div that wraps your elements.
        /// </summary>
        [Parameter] public string ItemWrapperClass { get; set; }

        /// <summary>
        /// If set items dropped are copied to this dropzone and are not removed from their source.
        /// </summary>
        [Parameter] public Func<TItem, TItem> CopyItem { get; set; }

        [Parameter] public Func<IList<TItem>, IList<TItem>> Sorter { get; set; }

        private void OnDropItemOnSpacing(int newIndex)
        {
            if (!IsDropAllowed())
            {
                this.Reset();
                return;
            }

            var activeItem = this.ActiveItem;
            var oldIndex = Items.IndexOf(activeItem);

            if (oldIndex == -1) // item not present in target dropzone
            {
                if (CopyItem == null)
                {
                    this.Items.Remove(activeItem);
                }
            }
            else // same dropzone drop
            {
                Items.RemoveAt(oldIndex);
                // the actual index could have shifted due to the removal
                if (newIndex > oldIndex) newIndex--;
            }

            if (CopyItem == null)
            {
                Items.Insert(newIndex, activeItem);
            }
            else
            {
                Items.Insert(newIndex, CopyItem(activeItem));
            }

            //Operation is finished
            this.Reset();
            OnItemDrop.InvokeAsync(activeItem);
        }

        private bool IsMaxItemLimitReached()
        {
            var activeItem = this.ActiveItem;

            return (!Items.Contains(activeItem) && MaxItems.HasValue && MaxItems == Items.Count());
        }

        private string IsItemDragable(TItem item)
        {
            if (AllowsDrag == null) return "true";
            if (item == null) return "false";

            return AllowsDrag(item).ToString();
        }

        private bool IsItemAccepted()
        {
            if (Accepts == null) return true;

            return Accepts(this.ActiveItem, this.DragTargetItem);
        }

        private bool IsValidItem()
        {
            return this.ActiveItem != null;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        public string CheckIfDraggable(TItem item)
        {
            if (AllowsDrag == null) return "";
            if (item == null) return "";

            if (AllowsDrag(item)) return "";

            return "dz-dd-noselect";
        }
        public string CheckIfDragOperationIsInProgess()
        {
            var activeItem = this.ActiveItem;

            return activeItem == null ? "" : "dz-dd-inprogess";
        }

        public void OnDragEnd()
        {
            this.Reset();
        }

        public void OnDragEnter(TItem item)
        {
            var activeItem = this.ActiveItem;

            if (item.Equals(activeItem)) return;

            if (!IsValidItem()) return;

            if (IsMaxItemLimitReached()) return;

            if (!IsItemAccepted()) return;

            this.DragTargetItem = item;

            if (InstantReplace)
            {
                Swap(this.DragTargetItem, activeItem);
            }

            StateHasChanged();
        }

        public void OnDragLeave()
        {
            this.DragTargetItem = default;
            StateHasChanged();
        }

        public void OnDragStart(TItem item)
        {
            this.ActiveItem = item;
            StateHasChanged();
        }

        public string CheckIfItemIsInTransit(TItem item)
        {
            return item.Equals(this.ActiveItem) ? "dz-dd-in-transit no-pointer-events" : "";
        }

        public string CheckIfItemIsDragTarget(TItem item)
        {
            if (item.Equals(this.ActiveItem)) return "";

            if (item.Equals(this.DragTargetItem))
            {
                return IsItemAccepted() ? "dz-dd-dragged-over" : "dz-dd-dragged-over-denied";
            }

            return "";
        }

        private string GetClassesForDraggable()
        {
            var builder = new StringBuilder();

            builder.Append("dz-dd-draggable");

            if (!String.IsNullOrEmpty(ItemWrapperClass))
            {
                builder.AppendLine(" " + ItemWrapperClass);
            }

            return builder.ToString();
        }

        private string GetClassesForDropzone()
        {
            var builder = new StringBuilder();

            builder.Append("dz-dd-dropzone");

            if (!String.IsNullOrEmpty(Class))
            {
                builder.AppendLine(" " + Class);
            }

            return builder.ToString();
        }

        private string GetClassesForSpacing(int spacerId)
        {
            var builder = new StringBuilder();

            builder.Append("dz-dd-spacing");

            //if active space id and item is from another dropzone -> always create insert space
            if (this.ActiveSpacerId == spacerId && Items.IndexOf(this.ActiveItem) == -1)
            {
                builder.Append(" dz-dd-spacing-dragged-over");
            } // else -> check if active space id and that it is an item that needs space
            else if (this.ActiveSpacerId == spacerId && (spacerId != Items.IndexOf(this.ActiveItem)) && (spacerId != Items.IndexOf(this.ActiveItem) + 1))
            {
                builder.Append(" dz-dd-spacing-dragged-over");
            }

            return builder.ToString();
        }

        private bool IsDropAllowed()
        {
            var activeItem = this.ActiveItem;

            if (!IsValidItem())
            {
                return false;
            }

            if (IsMaxItemLimitReached())
            {
                OnItemDropRejectedByMaxItemLimit.InvokeAsync(activeItem);
                return false;
            }

            if (!IsItemAccepted())
            {
                OnItemDropRejected.InvokeAsync(activeItem);
                return false;
            }

            return true;
        }

        private void OnDrop()
        {
            if (!IsDropAllowed())
            {
                this.Reset();
                return;
            }

            var activeItem = this.ActiveItem;

            if (this.DragTargetItem == null) //no direct drag target
            {
                if (!Items.Contains(activeItem)) //if dragged to another dropzone
                {
                    if (CopyItem == null)
                    {
                        Items.Insert(Items.Count, activeItem); //insert item to new zone
                        this.Items.Remove(activeItem); //remove from old zone
                    }
                    else
                    {
                        Items.Insert(Items.Count, CopyItem(activeItem)); //insert item to new zone
                    }
                }
                else
                {
                    //what to do here?

                }
            }
            else // we have a direct target
            {
                if (!InstantReplace)
                {
                    Swap(this.DragTargetItem, activeItem); //swap target with active item
                    OnItemDroped?.Invoke(this.ActiveItem, this.DragTargetItem);
                }
            }

            this.Reset();
            StateHasChanged();
            OnItemDrop.InvokeAsync(activeItem);
        }

        private void Swap(TItem draggedOverItem, TItem activeItem)
        {
            var indexDraggedOverItem = Items.IndexOf(draggedOverItem);
            var indexActiveItem = Items.IndexOf(activeItem);

            if (indexActiveItem == -1) // item is new to the dropzone
            {
                //insert into new zone
                Items.Insert(indexDraggedOverItem + 1, activeItem);
                //remove from old zone
                this.Items.Remove(activeItem);
            }
            else if (InstantReplace) //swap the items
            {
                if (indexDraggedOverItem == indexActiveItem) return;
                TItem tmp = Items[indexDraggedOverItem];
                Items[indexDraggedOverItem] = Items[indexActiveItem];
                Items[indexActiveItem] = tmp;
            }
            else //no instant replace, just insert it after 
            {
                if (indexDraggedOverItem == indexActiveItem) return;
                var tmp = Items[indexActiveItem];
                Items.RemoveAt(indexActiveItem);
                Items.Insert(indexDraggedOverItem, tmp);
            }

        }

        public void Dispose()
        {
        }

        /// <summary>
        /// Currently Active Item
        /// </summary>
        public TItem ActiveItem { get; set; }

        /// <summary>
        /// The item the active item is hovering above.
        /// </summary>
        public TItem DragTargetItem { get; set; }

        /// <summary>
        /// Holds the id of the Active Spacing div
        /// </summary>
        public int? ActiveSpacerId { get; set; }

        /// <summary>
        /// Resets the service to initial state
        /// </summary>
        public void Reset()
        {
            ActiveItem = default;
            ActiveSpacerId = null;
           // Items = null;
            DragTargetItem = default;

        }        
    }
}
