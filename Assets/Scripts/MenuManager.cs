using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        DataHandler.Instance.LoadData();

        MainMenu.Instance.Initialize();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
