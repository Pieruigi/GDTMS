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
            // Prepare data 
            SaveRoot root = new SaveRoot();
            root.Fill();
            // Create json
            string json = JsonUtility.ToJson(root);
            Debug.Log($"Storing json:{json}");
            // Write data
            System.IO.File.WriteAllText(GetPath(slot), json);
        }

        public void LoadGame(int slot)
        {
            // Read file
            string json = System.IO.File.ReadAllText(GetPath(slot));
            Debug.Log($"Loading json:{json}");
            SaveRoot root = JsonUtility.FromJson<SaveRoot>(json);
            root.Explode();
        }
    }

}
