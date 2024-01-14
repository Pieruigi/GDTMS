using GDTMS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestWorkerCreation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Worker.CreateWorkers(99);
            Worker.DebugAll();
        }
    }
}
