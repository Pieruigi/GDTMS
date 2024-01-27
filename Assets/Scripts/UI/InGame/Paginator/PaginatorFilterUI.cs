using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDTMS.UI
{
    public abstract class PaginatorFilterUI : MonoBehaviour
    {
        public UnityAction<string> OnChanged;

        public abstract void Apply(ref List<object> items, string filterName);

        public abstract void Activate();

        public abstract void Deactivate();
    }

}
