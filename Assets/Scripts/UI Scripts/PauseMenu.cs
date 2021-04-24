using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ash.MyUtils;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    private PlayerControls controls;

    public GameObject firstSelected;
    
    public GameObject pauseMenu;
    public Dropdown resolutionDropdown;
    public Slider musicSlider, sfxSlider;
    public Toggle fullscreenToggle;
    public AudioSource musicAudio;
    public AudioSource[] sfxSource;
    
    private static readonly string InitialPlayThrough = "InitialPlayThrough";
    private static readonly string MusicPref = "MusicPref";
    private static readonly string SfxPref = "SfxPref";
    private static readonly string ResolutionPref = "ResolutionPref";
    private static readonly string FullscreenPref = "FullscreenPref";

    private Gamepad gamepad;
    private bool isPaused = false;
    private int intialPlayThroughInt;
    private float musicFloat, sfxFloat;
    
    private Resolution[] resolutions;

    
    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.PauseMenu.performed += cxt => TogglePauseMenu();
        gamepad = Gamepad.current;
        if (gamepad != null)
        {
            ActivateControllerUI(true);
            ActivateKeyboardUI(false);
        }

        InputSystem.onDeviceChange +=
            (device, change) =>
            {
                switch (change)
                {
                    case InputDeviceChange.Added:
                        ActivateControllerUI(true);
                        ActivateKeyboardUI(false);
                        break;
                    case InputDeviceChange.Disconnected:
                        ActivateControllerUI(false);
                        ActivateKeyboardUI(true);
                        break;
                    case InputDeviceChange.Removed:
                        // Remove from Input System entirely; by default, Devices stay in the system once discovered.
                        break;
                    default:
                        // See InputDeviceChange reference for other event types.
                        break;
                }
            };
        pauseMenu.SetActive(false);
    }

    public void Start()
    {
        intialPlayThroughInt = PlayerPrefs.GetInt(InitialPlayThrough);

        if (intialPlayThroughInt == 0)
        {
            PlayerPrefs.SetInt(InitialPlayThrough, -1);
            InitializeAudioSettings();
            SaveAudioPrefs();
            ResolutionDropdownSetup();
            SelectDefaultResolution();
            SaveResolutionPrefs();
            SaveFullscreenPrefs();
        }
        else
        {
            ResolutionDropdownSetup();
            LoadSavedResolutionPrefs();
            LoadSavedAudioPrefs();
            LoadFullscreenPrefs();
        }
    }

    #region Public Methods

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
    
    public void UpdateMusicVolume()
    {
        musicAudio.volume = musicSlider.value;
    }

    public void UpdateSfxVolume()
    {
        for (int i = 0; i < sfxSource.Length; i++)
        {
            sfxSource[i].volume = sfxSlider.value;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartTheGame()
    {
        Debug.Log("Loading first scene");
    }

    public void SaveAndExit()
    {
        isPaused = false;
        SaveAudioPrefs();
        SaveResolutionPrefs();
        SaveFullscreenPrefs();
        pauseMenu.SetActive(false);
    }

    #endregion


    #region Private Methods

    private void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        if (isPaused)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstSelected);
        }
            
        if(!isPaused)
            SaveAndExit();
    }

    //private void ClosePauseMenu()
    //{
    //    SaveAndExit();
    //}

    private void InitializeAudioSettings()
    {
        musicFloat = .5f;
        sfxFloat = .5f;
        musicSlider.value = musicFloat;
        sfxSlider.value = sfxFloat;
    }

    private void SaveAudioPrefs()
    {
        PlayerPrefs.SetFloat(MusicPref, musicSlider.value);
        PlayerPrefs.SetFloat(SfxPref, sfxSlider.value);
    }

    private void LoadSavedAudioPrefs()
    {
        musicFloat = PlayerPrefs.GetFloat(MusicPref);
        musicSlider.value = musicFloat;
        sfxFloat = PlayerPrefs.GetFloat(SfxPref);
        sfxSlider.value = sfxFloat;
    }

    private void ResolutionDropdownSetup()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> resolutionOptions = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionOptions.Add(option);
        }
            
        resolutionDropdown.AddOptions(resolutionOptions);
    }

    private void SelectDefaultResolution()
    {
        resolutions = Screen.resolutions;
        int currentResolutionInd = 0;
        
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionInd = i;
            }
        }
        
        resolutionDropdown.value = currentResolutionInd;
        resolutionDropdown.RefreshShownValue();
    }

    private void SaveResolutionPrefs()
    {
        PlayerPrefs.SetInt(ResolutionPref, resolutionDropdown.value);
    }

    private void LoadSavedResolutionPrefs()
    {
        resolutionDropdown.value = PlayerPrefs.GetInt(ResolutionPref);
        resolutionDropdown.RefreshShownValue();
    }
    
    private void SaveFullscreenPrefs()
    {
        int fllInt = MyUtils.BoolToInt(fullscreenToggle.isOn);
        PlayerPrefs.SetInt(FullscreenPref, fllInt);
    }

    private void LoadFullscreenPrefs()
    {
        bool isFull = MyUtils.IntToBool(PlayerPrefs.GetInt(FullscreenPref));
        fullscreenToggle.isOn = isFull;
    }
    
    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SaveAudioPrefs();
            SaveResolutionPrefs();
            SaveFullscreenPrefs();
        }
    }

    private void ActivateControllerUI(bool isActive)
    {
        // set controller ui on or off
    }

    private void ActivateKeyboardUI(bool isActive)
    {
        // set keyboard ui on or off
    }

    #endregion

}
