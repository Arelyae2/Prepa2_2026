using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Button continueButton;

    [SerializeField]
    private Dropdown resolutionsDropdown;

    [SerializeField]
    private Dropdown qualitiesDropdown;


    [SerializeField]
    private Slider soundSlider;

    public Button clearSavedDataButton;

    [SerializeField]
    private GameObject optionsPanel;

    [SerializeField]
    private MainMenu mainMenu;

    public void Initialize()
    {
        soundSlider.value = Registry.data.audioVolume;

        bool saveFileExist = System.IO.File.Exists(Application.persistentDataPath + "/SavedData.json");
        continueButton.interactable = saveFileExist;
        clearSavedDataButton.interactable = saveFileExist;

        string[] qualities = QualitySettings.names;
        qualitiesDropdown.ClearOptions();

        List<string> qualityOptions = new List<string>();
        int currentQualityIndex = 0;

        for (int i = 0; i < qualities.Length; i++)
        {
            qualityOptions.Add(qualities[i]);

            if (i == QualitySettings.GetQualityLevel())
            {
                currentQualityIndex = i;
            }
        }

        qualitiesDropdown.AddOptions(qualityOptions);
        qualitiesDropdown.value = currentQualityIndex;
        qualitiesDropdown.RefreshShownValue();

        Resolution[] resolutions = Screen.resolutions;
        resolutionsDropdown.ClearOptions();

        List<string> resolutionOptions = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " (" + resolutions[i].refreshRateRatio.value + "Hz)";
            resolutionOptions.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionsDropdown.AddOptions(resolutionOptions);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();

        optionsPanel.SetActive(false);
    }


    public void NewGameButton()
    {
        SceneManager.LoadScene("Main");
    }
    public void ContinueButton()
    {
        DataHandler.Instance.LoadData();
        SceneManager.LoadScene("Main");
    }   
    public void LoadMainMenuButton()
    {
        Time.timeScale = 1;
        Registry.paused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void ClickSaveButton()
    {
        DataHandler.Instance.SaveData();
    }

    public void ClickLoadButton()
    {
        DataHandler.Instance.LoadData();
        SceneManager.LoadScene("Main");
    }


    public void QuitGameButton()
    {
        Application.Quit();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = Screen.resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetVolume()
    {
        Registry.data.audioVolume = soundSlider.value;


    }

    public void ClearSavedData()
    {
        System.IO.File.Delete(Application.persistentDataPath + "/SavedData.json");
        continueButton.interactable = false;
        clearSavedDataButton.interactable = false;
    }

    public void EnableDisableOptionsPanel()
    {
        optionsPanel.SetActive(!optionsPanel.activeSelf);
        DataHandler.Instance.SaveData();
    }
}
