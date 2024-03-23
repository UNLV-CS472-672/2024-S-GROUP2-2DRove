using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;


public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;   

    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    void Start(){
        // Set the resolution dropdown to the current resolution
        setResolution();
        
    }

    // Set the volume of the audio mixer
    public void setVolume(float vol){
        audioMixer.SetFloat("MasterVolume", vol);
    }

    // Set the full screen mode for the game
    public void setFullscreen(bool isFullscreen){
        Screen.fullScreen = isFullscreen;
    }
  
    public void setResolution(){
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width 
                && resolutions[i].height == Screen.currentResolution.height){
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
}
