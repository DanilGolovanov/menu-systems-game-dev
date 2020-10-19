using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Resolution[] resolutions;
    public Dropdown resolutionDropdown;

    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider soundFXSlider;

    public Dropdown qualityDropdown;
    public Toggle fullscreenToggle;

    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions
            .Select(resolution => new Resolution { width = resolution.width, height = resolution.height, refreshRate = resolution.refreshRate })
            .Distinct()
            .ToArray();
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        //go through each resolution
        for (int i = 0; i < resolutions.Length; i++)
        {
            //build a string for displaying the resolution
            string option = resolutions[i].width + "x" + resolutions[i].height + " (" + resolutions[i].refreshRate + "hz)";
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                //save number of the current resolution
                currentResolutionIndex = i;
            }  
        }
        //setup dropdown
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, false);
    }

    #region Change settings 

    //This changes the screen from fullscreen to windowed
    public void SetFullScreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void ChangeQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetSoundFXVolume(float value)
    {
        mixer.SetFloat("SoundFXVol", value);
    }

    public void SetMusicVolume(float value)
    {
        mixer.SetFloat("MusicVol", value);
    }

    #endregion

    #region Save and load player prefs

    public void SavePlayerPrefs()
    {
        //PlayerPrefs.SetInt("quality", QualitySettings.GetQualityLevel());
        PlayerPrefs.SetInt("quality", qualityDropdown.value);

        PlayerPrefs.SetInt("resolution", resolutionDropdown.value);

        if (fullscreenToggle.isOn)
        {
            PlayerPrefs.SetInt("fullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("fullscreen", 0);
        }

        float musicVol;
        if (mixer.GetFloat("MusicVol", out musicVol))
        {
            PlayerPrefs.SetFloat("MusicVol", musicVol);
        }

        float soundFXVol;
        if (mixer.GetFloat("SoundFXVol", out soundFXVol))
        {
            PlayerPrefs.SetFloat("SoundFXVol", soundFXVol);
        }

        PlayerPrefs.Save();
    }

    public void LoadPlayerPrefs()
    {
        //load quality
        if (PlayerPrefs.HasKey("quality"))
        {
            int quality = PlayerPrefs.GetInt("quality");
            qualityDropdown.value = quality;
            if (QualitySettings.GetQualityLevel() != quality)
            {
                ChangeQuality(quality);
            }
        }

        //load full screen
        if (PlayerPrefs.HasKey("fullscreen"))
        {
            if (PlayerPrefs.GetInt("fullscreen") == 0)
            {
                fullscreenToggle.isOn = false;
            }
            else
            {
                fullscreenToggle.isOn = true;
            }
        }

        //load audio sliders
        if (PlayerPrefs.HasKey("MusicVol"))
        {
            float musicVol = PlayerPrefs.GetFloat("MusicVol");
            musicSlider.value = musicVol;
            mixer.SetFloat("MusicVol", musicVol);
        }

        if (PlayerPrefs.HasKey("SoundFXVol"))
        {
            float soundFXVol = PlayerPrefs.GetFloat("SoundFXVol");
            soundFXSlider.value = soundFXVol;
            mixer.SetFloat("SoundFXVol", soundFXVol);
        }

        //------SET DEFAULT SETTINGS OTHERWISE (if nothing to load)
        if (!PlayerPrefs.HasKey("fullscreen"))
        {
            PlayerPrefs.SetInt("fullscreen", 1);
            Screen.fullScreen = true;

        }
        else
        {
            if (PlayerPrefs.GetInt("fullscreen") == 0)
            {
                Screen.fullScreen = false;
            }
            else
            {
                Screen.fullScreen = true;
            }
        }

        if (!PlayerPrefs.HasKey("quality"))
        {
            PlayerPrefs.SetInt("quality", 5);
            QualitySettings.SetQualityLevel(5);
        }
        else
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality"));
        }
        PlayerPrefs.Save();
    }

    #endregion
}
