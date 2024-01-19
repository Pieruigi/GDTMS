using GDTMS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestWorkerCreation : MonoBehaviour
{
    [SerializeField]
    WorkerManager wm = WorkerManager.Instance;

    //[SerializeField]
    //FinanceManager fm;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(FinanceManager.Instance);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            WorkerManager.Instance.CreateOrUpdateSearchList(99);
            //WorkerManager.Instance.DebugAll();
        }
    }
}
