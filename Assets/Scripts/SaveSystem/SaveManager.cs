using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS.SaveSystem
{
    public class SaveManager
    {
        static SaveManager instance;
        public static SaveManager Instance
        {
            get { if (instance == null) instance = new SaveManager(); return instance; }
        }

        int slotCount = 3;

        string saveNameFormat = "{0}.sav";

        SaveManager() 
        {

        }

        string GetPath(int slotIndex)
        {
            return System.IO.Path.Combine(Application.persistentDataPath, string.Format(saveNameFormat, slotIndex));
        }

        public void SaveGame(int slot)
        {
            string path = GetPath(slot);
            string json = JsonUtility.ToJson(new WorkerData(12, true));
            Debug.Log($"Json:{json}");
            System.IO.File.WriteAllText(path, json);
        }

        public void LoadGame(int slot)
        {

        }
    }

}
