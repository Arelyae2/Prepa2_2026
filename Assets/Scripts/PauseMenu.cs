using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isMenuOpened = false;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject optionsMenu;


    private void Awake()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        if (isMenuOpened)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

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
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    void Unpause()
    {
        Registry.paused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
