using GDTMS;
using GDTMS.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaveSystem : MonoBehaviour
{
    [SerializeField]
    WorkerFinder wm = WorkerFinder.Instance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SaveManager.Instance.SaveGame(0);

        if (Input.GetKeyDown(KeyCode.A))
            SaveManager.Instance.LoadGame(0);
    }
}
