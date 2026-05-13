using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    public static DataHandler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Initialize()
    {
        LoadData();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveData();
        }
        if(Input.GetKeyDown(KeyCode.F9))
        {
            LoadData();
        }
    }

   public void SaveData()
    {
        Debug.Log("Saving");

        if(PlayerController.Instance)
        {
            Registry.data.playerPosition = PlayerController.Instance.transform.position;
        }

        if(PlayerInteractor.Instance)
        {
            Registry.data.interactiveObjectsData.Clear();
            foreach (InteractiveObject obj in PlayerInteractor.Instance.interactiveObjects)
            {
                InteractiveObjectData objData = new InteractiveObjectData();
                objData.objectName = obj.objectName;
                objData.interactionID = obj.interactionID;
                Registry.data.interactiveObjectsData.Add(objData);
            }
        }

        Registry.data.saved = true;

        string jsonData = JsonUtility.ToJson(Registry.data);
        string filepath = Application.persistentDataPath + "/SavedData.json";

        File.WriteAllText(filepath, jsonData);

    }
    public void LoadData()
    {
        Debug.Log("Loading");

        string filepath = Application.persistentDataPath + "/SavedData.json";

        if (File.Exists(filepath))
        {
            string jsonData = System.IO.File.ReadAllText(filepath);
            Registry.data = JsonUtility.FromJson<SavedData>(jsonData);
        }
        else
        {
            ResetData();
        }
    }

    public void ResetData()
    {
        Debug.Log("Resetting");

        Registry.data = new SavedData();
        
    }

}

[System.Serializable]
public class SavedData
{
    public bool saved;
    public Vector3 playerPosition;
    public float audioVolume = 1f;
    public string currentCheckpointName;

    public List<InteractiveObjectData> interactiveObjectsData = new List<InteractiveObjectData>();
}

[System.Serializable]
public class InteractiveObjectData
{
    public string objectName;
    public int interactionID;
}