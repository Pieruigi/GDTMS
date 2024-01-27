using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS.UI
{
    public class WorkerFinderUI : PaginatorUI
    {
        protected override IList<object> GetItemList()
        {
            //return (IList<object>)WorkerFinder.Instance.SearchList;
            List<object> ret = new List<object>();
            foreach (var w in WorkerFinder.Instance.SearchList)
                ret.Add(w);
            return ret.AsReadOnly();
        }
    }

}
