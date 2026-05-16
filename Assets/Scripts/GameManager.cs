using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    Vector3 basePlayerPos;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1;
        Registry.paused = false;
        DataHandler.Instance.Initialize();

        if (Registry.data.saved)
        {
            Debug.Log("Found saved data - applying player pos");
            Debug.Log("Player pos from data: " + Registry.data.playerPosition);

            StartCoroutine(ForcePlayerSavedPlacement());
        }
        else
        {
            Debug.Log("Did not find save data");
            basePlayerPos = PlayerController.Instance.transform.position;
        }


        Initialize();
    }

    IEnumerator ForcePlayerSavedPlacement()
    {
        for (int i = 0; i < 20; i++)
        {
            basePlayerPos = Registry.data.playerPosition;
            PlayerController.Instance.transform.position = Registry.data.playerPosition;
            yield return new WaitForEndOfFrame();
        }
    }

    public virtual void Initialize()
    {
        PlayerInteractor.Instance.Initialize();
        HUD.Instance.Initialize();

        if (MainMenu.Instance != null)
        {
            MainMenu.Instance.Initialize();
        }
    }

    private void Update()
    {
        //player
        if (!HUD.Instance.Displaying())
        {
            PlayerController.Instance.PlayerControlUpdate();

            CameraController.Instance.LookUpdate();
            CameraController.Instance.AnimationUpdate();

            PlayerInteractor.Instance.SelectionUpdate();
            PlayerInteractor.Instance.InteractionUpdate();
        }

        PlayerObjectManager.Instance.HoldingUpdate();
        PlayerObjectAnimation.Instance.AnimationUpdate();

        CursorController.Instance.DisplayUpdate();
        HUD.Instance.DisplayUpdate();
    }



    private void FixedUpdate()
    {
        if (!HUD.Instance.Displaying())
        {
            PlayerController.Instance.MoveUpdate();
        }
        else
        {
            PlayerController.Instance.StopUpdate();
        }
    }

    public void SetCheckpoint(string objectName)
    {
        Registry.data.currentCheckpointName = objectName;
    }

    public void KillPlayer()
    {
        Debug.Log("Killed player");

        if(PlayerInteractor.Instance.InteractiveObjectFromName(Registry.data.currentCheckpointName) != null)
        {
            PlayerController.Instance.transform.position = PlayerInteractor.Instance.InteractiveObjectFromName(Registry.data.currentCheckpointName).transform.position;
            return;
        }

        PlayerController.Instance.transform.position = basePlayerPos;

    }
}
