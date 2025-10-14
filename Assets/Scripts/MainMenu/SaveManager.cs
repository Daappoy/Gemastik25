
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    private void Start()
    {
        LoadSave();
    }

    private void SaveGame(){
        SaveData save = new SaveData();

        string filePath = Application.persistentDataPath + "/save.json";
        string currentSaveData = JsonUtility.ToJson(save);
        Debug.Log("Saving to: " + filePath);
        System.IO.File.WriteAllText(filePath, currentSaveData);
        Debug.Log("Saved successfully");
    }

    public void LoadSave(){
        string filePath = Application.persistentDataPath + "/save.json";
        if(!System.IO.File.Exists(filePath)){
            Debug.Log("Save file not found");
            SaveGame();
            Debug.Log("Created new save file");
            string currentSaveData = System.IO.File.ReadAllText(filePath);
            SaveData save = JsonUtility.FromJson<SaveData>(currentSaveData);
            Debug.Log("Successfully retrieved the last save file");
            Debug.Log("Successfully loaded the game");
        }

    }
}

[System.Serializable]
public class SaveData{
    public bool PowerIsOff; 
    public SaveData()
    { 
        PowerIsOff = false;
    }
    
}
