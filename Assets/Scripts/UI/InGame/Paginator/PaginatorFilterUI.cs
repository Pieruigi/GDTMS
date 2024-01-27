using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDTMS.UI
{
    public abstract class PaginatorFilterUI : MonoBehaviour
    {
        /// <summary>
        /// Called when filter changes: paginator ui should register on this event
        /// </summary>
        public UnityAction<string> OnChanged;

        /// <summary>
        /// Called by the paginator ui when the filter has changed
        /// </summary>
        /// <param name="items"></param>
        /// <param name="filterName"></param>
        public abstract void Apply(ref List<object> items, string filterName);

        /// <summary>
        /// Init the filter
        /// </summary>
        public abstract void Reset();

        /// <summary>
        /// Reset the filter
        /// </summary>
        //public abstract void Deactivate();
    }

}
