using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GDTMS.UI
{
    public class OnDutyFinderUI : PaginatorUI
    {
        protected override IList<object> GetItemList()
        {
            if (!WorkerManager.Instance)
            {
                return new List<object>();
            }
            else
            {
                List<object> ret = new List<object>();
                foreach (var ag in WorkerManager.Instance.Agreements)
                    ret.Add(ag.Worker);
                return ret;
            }
                
        }
    }

}
