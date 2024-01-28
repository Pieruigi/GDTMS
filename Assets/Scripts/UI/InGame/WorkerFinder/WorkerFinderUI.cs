using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GDTMS.UI
{
    public class WorkerFinderUI : PaginatorUI
    {
        protected override IList<object> GetItemList()
        {
            if (!WorkerFinder.Instance)
                return new List<object>();
            else
                return WorkerFinder.Instance.SearchList.Cast<object>().ToList().AsReadOnly();
        }
    }

}
