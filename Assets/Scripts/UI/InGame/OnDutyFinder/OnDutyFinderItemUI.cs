using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS.UI
{
    public class OnDutyFinderItemUI : PaginatorItemUI
    {
        public override void Init(object item)
        {
            base.Init(item);

            Debug.Log("New on duty item");
        }
    }

}
