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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            WorkstationAsset asset = assets[Random.Range(0, assets.Count)];
            Debug.Log($"Buying workstation {asset.name}");
            if (FinanceManager.Instance.FoundsAreAvailable(asset.Price))
                WorkstationManager.Instance.BuyWorkstation(asset);
            if(HRManager.Instance.Agreements.Count > 0)
            {
                WorkerAgreement wa = HRManager.Instance.Agreements[Random.Range(0, HRManager.Instance.Agreements.Count)];
                
                Workstation w = WorkstationManager.Instance.Workstations[WorkstationManager.Instance.Workstations.Count - 1];
                Debug.Log($"Assign workstation {w.Name} to {wa.Worker.Name}");
                WorkstationManager.Instance.AssignWorkstation(w, wa.Worker);
            }
            
        }
    }
}
