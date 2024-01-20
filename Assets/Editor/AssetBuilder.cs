using GDTMS.Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetBuilder
{
    
    [MenuItem("Assets/Create/Scriptables/SkillAsset")]
    public static void CreateSkillAsset()
    {
        SkillAsset asset = ScriptableObject.CreateInstance<SkillAsset>();

        string name = "Skill.asset";

        string folder = System.IO.Path.Combine("Assets/Resources", SkillAsset.ResourceFolder);

        if (!System.IO.Directory.Exists(folder))
            System.IO.Directory.CreateDirectory(folder);

        AssetDatabase.CreateAsset(asset, System.IO.Path.Combine(folder, name));

        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    [MenuItem("Assets/Create/Scriptables/SkillCategoryAsset")]
    public static void CreateSkillCategoryAsset()
    {
        SkillTypeAsset asset = ScriptableObject.CreateInstance<SkillTypeAsset>();

        string name = "SkillCategory.asset";

        string folder = System.IO.Path.Combine("Assets/Resources", SkillTypeAsset.ResourceFolder);

        if (!System.IO.Directory.Exists(folder))
            System.IO.Directory.CreateDirectory(folder);

        AssetDatabase.CreateAsset(asset, System.IO.Path.Combine(folder, name));

        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    [MenuItem("Assets/Create/Scriptables/WorkstationAsset")]
    public static void CreateWorkstationAsset()
    {
        WorkstationAsset asset = ScriptableObject.CreateInstance<WorkstationAsset>();

        string name = "Workstation.asset";

        string folder = System.IO.Path.Combine("Assets/Resources", WorkstationAsset.ResourceFolder);

        if (!System.IO.Directory.Exists(folder))
            System.IO.Directory.CreateDirectory(folder);

        AssetDatabase.CreateAsset(asset, System.IO.Path.Combine(folder, name));

        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
