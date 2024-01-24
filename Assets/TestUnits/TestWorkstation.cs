using GDTMS;
using GDTMS.Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWorkstation : MonoBehaviour
{
    List<WorkstationAsset> assets;

    private void Awake()
    {
        assets = new List<WorkstationAsset>(Resources.LoadAll<WorkstationAsset>(WorkstationAsset.ResourceFolder));
    }

    // Start is called before the first frame update
    //void Start()
    //{
    //    List<int> l = new List<int>();
    //    l.Add(34);
    //    l.Add(12);
    //    l.Add(48);
    //    l.Add(9);
    //    foreach (var i in l)
    //        Debug.Log(i);
    //    Debug.Log("Sorting...");
    //    l.Sort(CompareDec);
    //    foreach (var i in l)
    //        Debug.Log(i);
    //}

    //static int CompareInc(int a, int b)
    //{
    //    if (a > b)
    //        return 1;
    //    else if (a == b)
    //        return 0;
    //    else
    //        return -1;
    //}

    //static int CompareDec(int a, int b)
    //{
    //    if (a > b)
    //        return -1;
    //    else if (a == b)
    //        return 0;
    //    else
    //        return 1;
    //}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            WorkstationAsset asset = assets[Random.Range(0, assets.Count)];
            Debug.Log($"Buying workstation {asset.name}");
            if (FinanceManager.Instance.FoundsAreAvailable(asset.Price))
                WorkstationManager.Instance.BuyWorkstation(asset);
            if(WorkerManager.Instance.Agreements.Count > 0)
            {
                WorkerAgreement wa = WorkerManager.Instance.Agreements[Random.Range(0, WorkerManager.Instance.Agreements.Count)];
                
                Workstation w = WorkstationManager.Instance.Workstations[WorkstationManager.Instance.Workstations.Count - 1];
                Debug.Log($"Assign workstation {w.Name} to {wa.Worker.Name}");
                WorkstationManager.Instance.AssignWorkstation(w, wa.Worker);
            }
            
        }
    }
}
