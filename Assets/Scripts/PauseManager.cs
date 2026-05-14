using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject optionsMenu;


    private void Awake()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Cursor.visible = Registry.paused;

        Cursor.lockState = Registry.paused ? CursorLockMode.None : CursorLockMode.Locked;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Registry.paused) Unpause();
            else
            {
                Pause();
            }
        }

    }

    void Pause()
    {
        Registry.paused = true;
        mainMenu.SetActive(true);
        Time.timeScale = 0;
    }

    void Unpause()
    {
        Registry.paused = false;
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
