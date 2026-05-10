using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private MainMenu mainMenu;


    private void Start()
    {
        if(MainMenu.loadSavedData)
        {
            LoadData();
        }
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
        SavedData savedData = new SavedData
        {
            playerPositions = playerTransform.position,
        };
        string jsonData = JsonUtility.ToJson(savedData);
        string filepath = Application.persistentDataPath + "/SavedData.json";
        Debug.Log(filepath);
        System.IO.File.WriteAllText(filepath, jsonData);
        Debug.Log("sauvegarde efectuée");

        mainMenu.continueButton.interactable = true;
        mainMenu.clearSavedDataButton.interactable = true;
    }
    public void LoadData()
    { 
        string filepath = Application.persistentDataPath + "/SavedData.json";
        string jsonData = System.IO.File.ReadAllText(filepath);

        SavedData savedData = JsonUtility.FromJson<SavedData>(jsonData);

        //chargement des données
        playerTransform.position = savedData.playerPositions;

        Debug.Log("chargement terminé");
    }
    public class SavedData
    {
        public Vector3 playerPositions;
    }
}