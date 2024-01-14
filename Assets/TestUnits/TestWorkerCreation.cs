using GDTMS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestWorkerCreation : MonoBehaviour
{
    [SerializeField]
    WorkerManager wm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            wm = WorkerManager.Instance;
            WorkerManager.Instance.CreateWorkers(99);
            WorkerManager.Instance.DebugAll();
        }
    }
}
