using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS.UI
{
    public abstract class PaginatorItemUI : MonoBehaviour
    {
        object item;
        protected object Item
        {
            get { return item; }
        }

        protected virtual void Awake()
        {

        }

        protected virtual void Start()
        {

        }

        public virtual void Init(object item)
        {
            this.item = item;
        }
    }

}
