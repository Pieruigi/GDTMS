using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDTMS.UI
{
    public abstract class PaginatorFilterUI : MonoBehaviour
    {
        PaginatorUI paginatorUI;

      
        /// <summary>
        /// Called by the paginator ui
        /// </summary>
        /// <param name="items"></param>        
        public abstract void Apply(ref List<object> items);

        protected abstract void Reset();

        protected virtual void Awake()
        {
            paginatorUI = GetComponentInParent<PaginatorUI>();
            paginatorUI.UseExternalFilter = true;
        }

        /// <summary>
        /// Called when filter changes
        /// </summary>
        protected virtual void Apply()
        {
            paginatorUI.ApplyFilter(this);
        }

        protected virtual void OnEnable()
        {
            Debug.Log("BUG - Reset");
            Reset();
        }
    }

}
